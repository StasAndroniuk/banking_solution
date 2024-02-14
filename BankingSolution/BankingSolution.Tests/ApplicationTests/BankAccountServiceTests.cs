using BankingSolution.Domain.Exceptions;
using BankingSolution.Domain.IntermediateModels;
using BankingSolution.Infrustructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BankingSolution.Tests.ApplicationTests
{
    public class BankAccountServiceTests : BaseTest
    {
        [Test]
        public async Task GetBankAccountListAsync_UseCorrectArgs_ReturnsAccounts()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "UA213223130000026007233566001",
                InitialBalance = 0,
                OwnerBirthday = DateTime.Now,
                OwnerFirstname = "name",
                OwnerLastname = "lastname",
                PhoneNumber = "1234567890",
            };
            await service.CreateBankAccountAsync(accountCreationDetails);

            var accounts = await service.GetBankAccountListAsync();

            accounts.Any(a => 
                a.Iban == accountCreationDetails.Iban && 
                a.AccountOwner.Firstname == accountCreationDetails.OwnerFirstname &&
                a.AccountOwner.LastName == accountCreationDetails.OwnerLastname).Should().BeTrue();
        }

        [Test]
        public async Task CreateBankAccountAsync_IncorrectIban_ThrowsInvalidIbanException()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "3287783024dn73d42d34n2",
                InitialBalance = 0,
                OwnerBirthday = DateTime.Now,
                OwnerFirstname = "name1",
                OwnerLastname = "lastname1",
                PhoneNumber = "1234567890",
            };
            var action = async () => await service.CreateBankAccountAsync(accountCreationDetails);

            await action.Should().ThrowAsync<InvalidIbanException>();
        }

        [Test]
        public async Task CreateBankAccountAsync_UseExistingAccountOwner_AccountCreatedWithExistingAccountOwner()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "AD1400080001001234567890",
                InitialBalance = 0,
                OwnerBirthday = DateTime.Now,
                OwnerFirstname = "name2",
                OwnerLastname = "lastname2",
                PhoneNumber = "1234567890",
            };
            await service.CreateBankAccountAsync(accountCreationDetails);
            accountCreationDetails.Iban = "AT483200000012345864";
            await service.CreateBankAccountAsync(accountCreationDetails);

            var accounts = await service.GetBankAccountListAsync();
            var secondAccount = accounts.FirstOrDefault(a => a.Iban == accountCreationDetails.Iban);

            secondAccount.Should().NotBeNull();
            accounts.Where(a => a.AccountOwner.Firstname == accountCreationDetails.OwnerFirstname && a.AccountOwner.LastName == accountCreationDetails.OwnerLastname).Count().Should().Be(2);   
        }

        [Test]
        public async Task CreateBankAccountAsync_DuplicateIban_ThrowsDuplicateIbanException()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "AL35202111090000000001234567",
                InitialBalance = 0,
                OwnerBirthday = DateTime.Now,
                OwnerFirstname = "name3",
                OwnerLastname = "lastname3",
                PhoneNumber = "1234567890",
            };
            await service.CreateBankAccountAsync(accountCreationDetails);
            var action = async () => await service.CreateBankAccountAsync(accountCreationDetails);

            await action.Should().ThrowAsync<DuplicateIbanException>();
        }

        [Test]
        public async Task GetBankAccountByIdAsync_UseNotExistingId_ThrowsBankAccountNotFoundException()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var action = async () => await service.GetBankAccountByIdAsync(2311233);

            await action.Should().ThrowAsync<BankAccountNotFoundException>();
        }

        [Test]
        public async Task GetBankAccountByIdAsync_UseCorrectArgs_ReturnsAccount()
        {
            var service = TestServiceProvider.GetService<IBankAccountService>()!;

            var accountCreationDetails = new BankAccountCreationDetails()
            {
                Iban = "AZ77VTBA00000000001234567890",
                InitialBalance = 0,
                OwnerBirthday = DateTime.Now,
                OwnerFirstname = "name4",
                OwnerLastname = "lastname4",
                PhoneNumber = "1234567890",
            };
            await service.CreateBankAccountAsync(accountCreationDetails);

            var accounts = await service.GetBankAccountListAsync();

            var accountId = accounts.First(a =>
                a.Iban == accountCreationDetails.Iban &&
                a.AccountOwner.Firstname == accountCreationDetails.OwnerFirstname &&
                a.AccountOwner.LastName == accountCreationDetails.OwnerLastname).Id;

            var account = await service.GetBankAccountByIdAsync(accountId);

            account.Should().NotBeNull();
        }
    }
}
