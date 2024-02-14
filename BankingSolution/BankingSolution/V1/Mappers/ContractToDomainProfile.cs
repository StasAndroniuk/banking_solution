using AutoMapper;
using BankingSolution.Contract.Rest.V1.Transport.Requests;
using BankingSolution.Domain.IntermediateModels;

namespace BankingSolution.V1.Mappers
{
    public class ContractToDomainProfile : Profile
    {
        public ContractToDomainProfile()
        {
            CreateMap<CreateBankAccountRequest, BankAccountCreationDetails>();
        }
    }
}
