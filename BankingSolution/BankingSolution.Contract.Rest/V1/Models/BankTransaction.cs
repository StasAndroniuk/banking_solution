namespace BankingSolution.Contract.Rest.V1.Models
{
    public class BankTransaction
    {
        /// <summary>
        /// Id of transaction
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Amounty of transaction
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// transaction creation date
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Iban where funds were transfered.
        /// </summary>
        public string DestionationIban { get; set; }

        /// <summary>
        /// Source iban from which funds were transfered. 
        /// Filled in case of transfering from one account to another.
        /// </summary>
        public string? SourceIban { get; set; }
    }
}
