namespace BankingSolution.Contract.Rest.V1.Models
{
    public class BankAccount
    {
        public uint Id { get; set; }

        /// <summary>
        /// Information about account owner.
        /// </summary>
        public BankAccountOwner AccountOwner { get; set; }

        /// <summary>
        /// Iban number.
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// Current value bank account balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
