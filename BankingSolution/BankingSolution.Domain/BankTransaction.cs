using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Domain
{
    public class BankTransaction
    {
        public BankTransaction(decimal amount, BankAccount destionationBankAccount, BankAccount? sourceBankAccount = null)
        {
            Amount = amount;
            DestionationBankAccount = destionationBankAccount;
            SourceBankAccount = sourceBankAccount;
            CreatedOn = DateTime.Now;
        }

        private BankTransaction() { }

        [Key]
        public uint Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedOn { get; set; }

        public BankAccount DestionationBankAccount { get; set; }

        public BankAccount? SourceBankAccount { get; set; }
    }
}
