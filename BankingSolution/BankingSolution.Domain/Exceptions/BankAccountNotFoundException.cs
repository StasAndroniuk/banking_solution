namespace BankingSolution.Domain.Exceptions
{
    public class BankAccountNotFoundException : Exception
    {
        public BankAccountNotFoundException(string message) : base(message) { }
    }
}
