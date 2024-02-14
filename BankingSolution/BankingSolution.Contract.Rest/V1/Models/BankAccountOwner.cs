namespace BankingSolution.Contract.Rest.V1.Models
{
    public class BankAccountOwner
    {
        /// <summary>
        /// Owner Id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Owner firstname
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Owner lastname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Owner birthday
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Owner phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
