namespace BankingSolution.Domain
{
    /// <summary>
    /// Entity that contains information about bank account owner.
    /// </summary>
    public class BankAccountOwner
    {
        public BankAccountOwner(string firstname, string lastName, DateTime birthday, string phoneNumber)
        {
            Firstname = firstname;
            LastName = lastName;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
        }

        private BankAccountOwner() { }
        /// <summary>
        /// Unique identifier
        /// </summary>
        public uint Id { get; set; }
        
        /// <summary>
        /// Owner firstname.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Owner lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// owner date of birth.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Owner phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
