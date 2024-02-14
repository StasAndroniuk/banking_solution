using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Contract.Rest.V1.Transport.Requests
{
    /// <summary>
    /// Request used to create new bank account
    /// </summary>
    public class CreateBankAccountRequest
    {
        /// <summary>
        /// Iban value
        /// </summary>
        [Required]
        [RegularExpression("[a-zA-Z0-9]+")]
        public string Iban { get; set; }

        /// <summary>
        /// Owner firstname
        /// </summary>
        [Required]
        public string OwnerFirstname { get; set; }

        /// <summary>
        /// Owner lastname
        /// </summary>
        [Required]
        public string OwnerLastname { get; set; }

        /// <summary>
        /// Owner birthday
        /// </summary>
        [Required]
        public DateTime OwnerBirthday { get; set; }

        /// <summary>
        /// Owner phonenumber
        /// </summary>
        [Required]
        [RegularExpression("[0-9]+")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Acocunt initial balance.
        /// </summary>
        public decimal InitialBalance { get; set; }
    }
}
