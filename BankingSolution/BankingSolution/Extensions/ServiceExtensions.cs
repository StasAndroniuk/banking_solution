using BankingSolution.Application;
using BankingSolution.Infrustructure;
using BankingSolution.Repository;
using BankingSolution.V1.Mappers;
using IbanNet.DependencyInjection.ServiceProvider;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankingSolution.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddIbanNet();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ITransactionsService, TransactionsService>();
        }

        public static void ConfigureDbContext(this IServiceCollection services)
        {
            services.AddDbContext<BankingContext>(options => options.UseInMemoryDatabase("Banking"));
        }

        public static void ConfigureMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(
                mapperConfigurationExpression =>
                {
                    mapperConfigurationExpression.AddProfile<ContractToDomainProfile>();
                    mapperConfigurationExpression.AddProfile<DomainToContractProfile>();
                },
                Array.Empty<Assembly>());
        }
    }
}
