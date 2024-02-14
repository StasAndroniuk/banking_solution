using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to get the list transactions done by specified account
    /// </summary>
    public class GetBankTransactionListByAccountIdRequest
    {
        /// <summary>
        /// Account id
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue)]
        public uint AccountId { get; set; }
    }
}
