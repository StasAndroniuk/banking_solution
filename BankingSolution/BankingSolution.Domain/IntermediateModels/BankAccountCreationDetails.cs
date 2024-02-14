using System.ComponentModel.DataAnnotations;

namespace BankingSolution.Domain.IntermediateModels
{
    public class BankAccountCreationDetails
    {
        public string Iban { get; set; }

        public string OwnerFirstname { get; set; }

        public string OwnerLastname { get; set; }

        public DateTime OwnerBirthday { get; set; }

        public string PhoneNumber { get; set; }

        public decimal InitialBalance { get; set; }
    }
}
