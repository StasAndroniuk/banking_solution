namespace BankingSolution.Contract.Rest.V1.Models
{
    public class BankAccountOwner
    {
        public uint Id { get; set; }

        public string Firstname { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }
    }
}
