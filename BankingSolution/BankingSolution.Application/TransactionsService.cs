using BankingSolution.Domain;
using BankingSolution.Domain.Exceptions;
using BankingSolution.Infrustructure;
using BankingSolution.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankingSolution.Application
{
    public class TransactionsService : ITransactionsService
    {
        private readonly BankingContext _bankingContext;
        private readonly ILogger<TransactionsService> _logger;

        public TransactionsService(BankingContext bankingContext, ILogger<TransactionsService> logger)
        {
            _bankingContext = bankingContext;
            _logger = logger;
        }

        public async Task<IEnumerable<BankTransaction>> GetBankTransactionsByAccountIdAsync(uint id, CancellationToken cancellationToken = default) =>
            await _bankingContext.Transactions.Include(t => t.SourceBankAccount).Include(t => t.DestionationBankAccount)
            .Where(t => t.DestionationBankAccount.Id == id)
            .ToListAsync(cancellationToken);

        public async Task MakeDepositFundsToAccountAsync(decimal amount, uint destinationAccountId, CancellationToken cancellationToken = default)
        {
            var account = await _bankingContext.BankAccounts.FirstOrDefaultAsync(a => a.Id == destinationAccountId, cancellationToken);
            if (account == null)
            {
                _logger.LogError($"bank account with iban: {destinationAccountId} not found");
                throw new BankAccountNotFoundException($"bank account with iban: {destinationAccountId} not found");
            }

            var bankTransaction = new BankTransaction(amount, account);
            account.Balance += Math.Round(amount, 2);
            await _bankingContext.AddAsync(bankTransaction, cancellationToken);
            await _bankingContext.SaveChangesAsync(cancellationToken);
        }

        public async Task MakeWithdrawFundsFromAcountAsync(decimal amount, uint sourceAccountId, CancellationToken cancellationToken = default)
        {
            var account = await _bankingContext.BankAccounts.FirstOrDefaultAsync(a => a.Id == sourceAccountId, cancellationToken);
            if (account == null)
            {
                _logger.LogError($"bank account with iban: {sourceAccountId} not found");
                throw new BankAccountNotFoundException($"bank account with iban: {sourceAccountId} not found");
            }

            if(account.Balance < amount)
            {
                _logger.LogError($"Account has not enough balance to withdraw {amount}");
                throw new InvalidAccountBalanceException($"Account has not enough balance to withdraw {amount}");
            }

            var bankTransaction = new BankTransaction(-amount, account);
            account.Balance -= Math.Round(amount, 2);

            await _bankingContext.AddAsync(bankTransaction, cancellationToken);
            await _bankingContext.SaveChangesAsync(cancellationToken);
        }

        public async Task TransferFundsBetweenAccountsAsync(decimal amount, uint sourceAccountId, uint destinationAccountId, CancellationToken cancellationToken = default)
        {
            var sourceAccount = await _bankingContext.BankAccounts.Include(a => a.AccountOwner).FirstOrDefaultAsync(a => a.Id == sourceAccountId, cancellationToken);
            if(sourceAccount == null)
            {
                _logger.LogError($"Bank account with ID: {sourceAccountId} not found");
                throw new BankAccountNotFoundException($"Bank account with ID: {sourceAccountId} not found");
            }

            var destinationAccount = await _bankingContext.BankAccounts.Include(a => a.AccountOwner).FirstOrDefaultAsync(a => a.Id == destinationAccountId, cancellationToken);
            if (destinationAccount == null)
            {
                _logger.LogError($"Bank account with ID: {destinationAccountId} not found");
                throw new BankAccountNotFoundException($"Bank account with ID: {destinationAccountId} not found");
            }

            if(sourceAccount.Balance < amount)
            {
                _logger.LogError("Source account balance is less than requested transfer amount");
                throw new InvalidAccountBalanceException("Source account balance is less than requested transfer amount");
            }

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            var debitBanktransaction = new BankTransaction(amount, destinationAccount, sourceAccount);
            var creditBanktransaction = new BankTransaction(-amount, sourceAccount);
            await _bankingContext.AddRangeAsync(new List<BankTransaction>() { debitBanktransaction, creditBanktransaction}, cancellationToken);
            await _bankingContext.SaveChangesAsync(cancellationToken);
        }
    }
}
