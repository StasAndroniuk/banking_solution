using BankingSolution.Application;
using BankingSolution.Infrustructure;
using BankingSolution.Repository;
using IbanNet;
using IbanNet.Validation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.ComponentModel.Design;

namespace BankingSolution.Tests
{
    internal class BaseTest
    {
        protected string IncorrectIban = "3287783024dn73d42d34n2";
        protected BaseTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            var ibanValidatorMock = new Mock<IIbanValidator>();
            ibanValidatorMock.Setup(x => x.Validate(It.IsAny<string>()))
                .Returns(() => new ValidationResult());
            ibanValidatorMock.Setup(x => x.Validate(IncorrectIban))
                .Returns(() => new ValidationResult() { Error = new ErrorResult("Incorrect")});

            services.AddDbContext<BankingContext>(options => options.UseInMemoryDatabase("Banking"));
            services.AddScoped<IServiceContainer, ServiceContainer>();
            services.AddScoped(prov => ibanValidatorMock.Object);
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITransactionsService, TransactionsService>();


            TestServiceProvider = services.BuildServiceProvider();
        }
        protected IServiceProvider TestServiceProvider { get; }
    }
}
