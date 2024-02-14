using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to make withdraw from specied account
    /// </summary>
    public class MakeWithdrawFundsRequest
    {
        /// <summary>
        /// Withdraw amount
        /// </summary>
        [Required]
        [Range(0.01, uint.MaxValue)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Account id
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue)]
        public uint AccountId { get; set; }
    }
}
