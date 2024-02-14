using BankingSolution.Domain.Exceptions;
using BankingSolution.Domain.IntermediateModels;
using BankingSolution.Infrustructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BankingSolution.Tests.ApplicationTests
{
    internal class TransactionsServiceTests : BaseTest
    {
        [Test]
        public async Task MakeDepositFundsToAccountAsync_NotExistingBankAccount_ThrowsBankAccountNotFoundException()
        {
            var service = TestServiceProvider.GetService<ITransactionsService>()!;

            var action = async ()=> await service.MakeDepositFundsToAccountAsync(1, 324234324);

            await action.Should().ThrowAsync<BankAccountNotFoundException>();
        }

        [Test]
        public async Task MakeDepositFundsToAccountAsync_UseCorrectAgrs_DepositMade()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;
            var destinationIban = "BH02CITI00001077181611";
            var depositbalance = 44.65m;
            var initialBalance = 2m;
            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = destinationIban,
                InitialBalance = initialBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };
            var accountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            await tranactionService.MakeDepositFundsToAccountAsync(depositbalance, accountId);

            var account = await accountService.GetBankAccountByIdAsync(accountId);
            var transactions = await tranactionService.GetBankTransactionsByAccountIdAsync(accountId);

            account.Should().NotBeNull();
            account.Balance.Should().Be(depositbalance + initialBalance);
            transactions.Any(t => t.Amount == depositbalance).Should().BeTrue();
        }

        [Test]
        public async Task MakeWithdrawFundsFromAcountAsync_UseNotExistingAccount_ThrowsBankAccountNotFoundException()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;

            var action = async () => await tranactionService.MakeWithdrawFundsFromAcountAsync(3m, 32879);

            await action.Should().ThrowAsync<BankAccountNotFoundException>();
        }

        [Test]
        public async Task MakeWithdrawFundsFromAcountAsync_UseTooBigAmount_ThrowsInvalidAccountBalanceException()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;
            var destinationIban = "BE71096123456769";
            var withdraw = 44.65m;
            var initialBalance = 2m;
            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = destinationIban,
                InitialBalance = initialBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };
            var accountId = await accountService.CreateBankAccountAsync(accountCreationDetails);
            var action = async () => await tranactionService.MakeWithdrawFundsFromAcountAsync(withdraw, accountId);

            await action.Should().ThrowAsync<InvalidAccountBalanceException>();

        }

        [Test]
        public async Task MakeWithdrawFundsFromAcountAsync_UseCorrectArgs_WithdrawMade()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;
            var destinationIban = "BA393385804800211234";
            var withdraw = 44.65m;
            var initialBalance = 82m;
            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = destinationIban,
                InitialBalance = initialBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };
            var accountId = await accountService.CreateBankAccountAsync(accountCreationDetails);
            await tranactionService.MakeWithdrawFundsFromAcountAsync(withdraw, accountId);
            var account = await accountService.GetBankAccountByIdAsync(accountId);
            var transactions = await tranactionService.GetBankTransactionsByAccountIdAsync(accountId);

            account.Should().NotBeNull();
            account.Balance.Should().Be(initialBalance - withdraw);
            
            transactions.Any().Should().BeTrue();
            var transaction = transactions.First();
            transaction.Amount.Should().Be(-withdraw);
        }

        [Test]
        public async Task GetBankTransactionsByAccountIdAsync_UseCorrectAgrs_ReturnsTransactions()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;
            var destinationIban = "BR1500000000000010932840814P2";
            var depositbalance = 44.65m;
            var initialBalance = 2m;
            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = destinationIban,
                InitialBalance = initialBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };
            var accountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            await tranactionService.MakeDepositFundsToAccountAsync(depositbalance, accountId);

            var transactions = await tranactionService.GetBankTransactionsByAccountIdAsync(accountId);

            transactions.Any(t => t.Amount == depositbalance).Should().BeTrue();
        }

        [Test]
        public async Task TransferFundsBetweenAccountsAsync_NotExistingSourceAccount_ThrowsBankAccountNotFoundException()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;

            var action = async () => await tranactionService.TransferFundsBetweenAccountsAsync(1m, 213213123, 213123123);

            await action.Should().ThrowAsync<BankAccountNotFoundException>();
        }

        [Test]
        public async Task TransferFundsBetweenAccountsAsync_NotExistingDestinationAccount_ThrowsBankAccountNotFoundException()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;


            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "BG18RZBB91550123456789",
                InitialBalance = 11111,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };

            var sourceAccountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            var action = async () => await tranactionService.TransferFundsBetweenAccountsAsync(1m, sourceAccountId, 213123123);

            await action.Should().ThrowAsync<BankAccountNotFoundException>();
        }

        [Test]
        public async Task TransferFundsBetweenAccountsAsync_LowSourceBalance_ThrowsBankAccountNotFoundException()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;


            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "BI1320001100010000123456789",
                InitialBalance = 1,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };

            var sourceAccountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "CR23015108410026012345",
                InitialBalance = 11111,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };

            var destionationAccountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            var action = async () => await tranactionService.TransferFundsBetweenAccountsAsync(111m, sourceAccountId, destionationAccountId);

            await action.Should().ThrowAsync<InvalidAccountBalanceException>();
        }

        [Test]
        public async Task TransferFundsBetweenAccountsAsync_UserCorrectArgs_TransferDone()
        {
            var tranactionService = TestServiceProvider.GetService<ITransactionsService>()!;
            var accountService = TestServiceProvider.GetService<IBankAccountService>()!;
            var fundsToTransfer = 11m;
            var sourceAccountBalance = 11111m;
            var destinationAccountBalance = 10m;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = Guid.NewGuid().ToString(),
                InitialBalance = sourceAccountBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };

            var sourceAccountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = Guid.NewGuid().ToString(),
                InitialBalance = destinationAccountBalance,
                OwnerBirthday = DateTime.Now.AddYears(-30),
                OwnerFirstname = Guid.NewGuid().ToString(),
                OwnerLastname = Guid.NewGuid().ToString(),
                PhoneNumber = "324234324",
            };

            var destionationAccountId = await accountService.CreateBankAccountAsync(accountCreationDetails);

            await tranactionService.TransferFundsBetweenAccountsAsync(fundsToTransfer, sourceAccountId, destionationAccountId);
            var sourceAccount = await accountService.GetBankAccountByIdAsync(sourceAccountId);
            var destionationAccount = await accountService.GetBankAccountByIdAsync(destionationAccountId);
            var sourceAccountTransactions = await tranactionService.GetBankTransactionsByAccountIdAsync(sourceAccountId);
            var destionationAccountTransactions = await tranactionService.GetBankTransactionsByAccountIdAsync(destionationAccountId);

            sourceAccount.Balance.Should().Be(sourceAccountBalance - fundsToTransfer);
            destionationAccount.Balance.Should().Be(destinationAccountBalance + fundsToTransfer);

            sourceAccountTransactions.First().Amount.Should().Be(-fundsToTransfer);
            destionationAccountTransactions.First().Amount.Should().Be(fundsToTransfer);

        }
    }
}
