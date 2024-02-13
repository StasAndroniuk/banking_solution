namespace BankingSolution.Domain
{
    /// <summary>
    /// Entity represents bank account and contains financial infromation.
    /// </summary>
    public class BankAccount
    {
        public BankAccount(BankAccountOwner accountOwner, string iban)
        {
            AccountOwner = accountOwner;
            Iban = iban;
            Balance = 0;
        }

        private BankAccount() { }
        /// <summary>
        /// Unique identifier.
        /// </summary>
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
