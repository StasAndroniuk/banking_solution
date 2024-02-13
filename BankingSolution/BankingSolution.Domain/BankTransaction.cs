namespace BankingSolution.Domain
{
    public class BankTransaction
    {
        public BankTransaction(decimal amount, BankAccount destionationBankAccount, BankAccount? sourceBankAccount)
        {
            Amount = amount;
            DestionationBankAccount = destionationBankAccount;
            SourceBankAccount = sourceBankAccount;
        }

        private BankTransaction() { }

        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public BankAccount DestionationBankAccount { get; set; }

        public BankAccount? SourceBankAccount { get; set; }
    }
}
