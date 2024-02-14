using BankingSolution.Contract.Rest.V1.Models;

namespace BankingSolution.Contract.Rest.V1.Transport.Responses
{
    public class GetBankTransactionListByAccountIdResponse
    {
        public IEnumerable<BankTransaction>? Transactions { get; set; }
    }
}
