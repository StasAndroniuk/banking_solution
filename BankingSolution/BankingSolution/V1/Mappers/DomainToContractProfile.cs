using AutoMapper;

namespace BankingSolution.V1.Mappers
{
    public class DomainToContractProfile : Profile
    {
        public DomainToContractProfile() 
        {
            CreateMap<Domain.BankAccountOwner, Contract.Rest.V1.Models.BankAccountOwner>();
            CreateMap<Domain.BankAccount, Contract.Rest.V1.Models.BankAccount>();
        }
    }
}
