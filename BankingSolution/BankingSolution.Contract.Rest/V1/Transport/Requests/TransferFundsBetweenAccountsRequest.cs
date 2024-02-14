using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to transfer funds between accounts
    /// </summary>
    public class TransferFundsBetweenAccountsRequest
    {
        /// <summary>
        /// Deposit amount
        /// </summary>
        [Required]
        [Range(0.01, uint.MaxValue)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Source account id
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue)]
        public uint SourceAccountId { get; set; }

        /// <summary>
        /// Destination account id
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue)]
        public uint DestinationAccountId { get; set; }
    }
}
