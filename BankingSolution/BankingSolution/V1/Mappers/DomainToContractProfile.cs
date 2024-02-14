using AutoMapper;

namespace BankingSolution.V1.Mappers
{
    public class DomainToContractProfile : Profile
    {
        public DomainToContractProfile() 
        {
            CreateMap<Domain.BankAccountOwner, Contract.Rest.V1.Models.BankAccountOwner>();
            CreateMap<Domain.BankAccount, Contract.Rest.V1.Models.BankAccount>();
            CreateMap<Domain.BankTransaction, Contract.Rest.V1.Models.BankTransaction>()
                .ForMember(x => x.SourceIban, opt => opt.MapFrom(x => x.SourceBankAccount == null ? null : x.SourceBankAccount.Iban))
                .ForMember(x => x.DestionationIban, opt => opt.MapFrom(x => x.DestionationBankAccount.Iban));
        }
    }
}
