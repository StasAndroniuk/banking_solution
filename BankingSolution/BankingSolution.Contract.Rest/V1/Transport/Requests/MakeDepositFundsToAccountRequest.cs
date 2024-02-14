using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to make a deposit for specified account
    /// </summary>
    public class MakeDepositFundsToAccountRequest
    {
        /// <summary>
        /// Deposit amount
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
