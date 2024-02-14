using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to get specific bank account by id.
    /// </summary>
    public class GetBankAccountRequest
    {
        /// <summary>
        /// Id of bank account
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue)]
        public uint Id { get; set; }
    }
}
