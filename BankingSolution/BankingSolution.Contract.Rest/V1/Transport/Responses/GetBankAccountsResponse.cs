using BankingSolution.Contract.Rest.V1.Models;

namespace BankingSolution.Contract.Rest.V1.Transport.Responses
{
    public class GetBankAccountsResponse
    {
        public IEnumerable<BankAccount>? BankAccounts { get; set; }
    }
}
