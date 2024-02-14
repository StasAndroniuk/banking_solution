using BankingSolution.Domain;
using BankingSolution.Domain.Exceptions;
using BankingSolution.Domain.IntermediateModels;
using BankingSolution.Infrustructure;
using BankingSolution.Repository;
using IbanNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankingSolution.Application
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankingContext _context;
        private readonly IIbanValidator _ibanValidator;
        private readonly ILogger<BankAccountService> _logger;

        public BankAccountService(BankingContext context, IIbanValidator ibanValidator, ILogger<BankAccountService> logger)
        {
            _context = context;
            _ibanValidator = ibanValidator;
            _logger = logger;
        }

        public async Task<uint> CreateBankAccountAsync(BankAccountCreationDetails details, CancellationToken cancellationToken = default)
        {
            var validationResult = _ibanValidator.Validate(details.Iban);
            if (!validationResult.IsValid)
            {
                _logger.LogError($"Incorrect iban {details.Iban}");
                throw new InvalidIbanException($"Incorrect iban {details.Iban}");
            }

            var duplicateAccount = await _context.BankAccounts.FirstOrDefaultAsync(a => a.Iban == details.Iban, cancellationToken);
            
            if (duplicateAccount != null)
            {
                _logger.LogError($"Bank account with iban {details.Iban} already exists");
                throw new DuplicateIbanException($"Bank account with iban {details.Iban} already exists");
            }

            var bankAccount = new BankAccount(
                new BankAccountOwner(
                    details.OwnerFirstname,
                    details.OwnerLastname,
                    details.OwnerBirthday,
                    details.PhoneNumber),
                details.Iban,
                details.InitialBalance);

            var existingAccountOwner = await _context.AccountOwners.FirstOrDefaultAsync(o =>
                o.Firstname == details.OwnerFirstname && o.LastName == details.OwnerLastname && o.Birthday == details.OwnerBirthday.Date,
                cancellationToken);
            if (existingAccountOwner != null)
            {
                _logger.LogInformation($"Found existing account owner {details.OwnerFirstname} {details.OwnerLastname}. Will use existing accoutn owner");
                bankAccount.AccountOwner = existingAccountOwner;
            }

            await _context.AddAsync(bankAccount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return bankAccount.Id;
        }

        public async Task<BankAccount> GetBankAccountByIdAsync(uint id, CancellationToken cancellationToken = default)
        {
            var bankAccount = await _context.BankAccounts.Include(a => a.AccountOwner).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (bankAccount == null)
            {
                _logger.LogError($"Bank account with ID:{id} not found");
                throw new BankAccountNotFoundException($"Bank account with ID:{id} not found");
            }
            return bankAccount;
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountListAsync(CancellationToken cancellationToken = default) =>
            await _context.BankAccounts.Include(a => a.AccountOwner).ToListAsync(cancellationToken);

    }
}
