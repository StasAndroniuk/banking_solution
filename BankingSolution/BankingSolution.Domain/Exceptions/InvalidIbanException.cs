namespace BankingSolution.Domain.Exceptions
{
    public class InvalidIbanException : Exception
    {
        public InvalidIbanException(string message) : base(message) { }
    }
}
