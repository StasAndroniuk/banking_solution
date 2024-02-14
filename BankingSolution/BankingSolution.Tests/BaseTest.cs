using BankingSolution.Application;
using BankingSolution.Infrustructure;
using BankingSolution.Repository;
using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace BankingSolution.Tests
{
    public class BaseTest
    {
        protected BaseTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContext<BankingContext>(options => options.UseInMemoryDatabase("Banking"));
            services.AddScoped<IServiceContainer, ServiceContainer>();
            services.AddIbanNet();
            services.AddScoped<IBankAccountService, BankAccountService>();


            TestServiceProvider = services.BuildServiceProvider();
        }
        protected IServiceProvider TestServiceProvider { get; }
    }
}
