namespace BankingSolution.Domain.Exceptions
{
    public class InvalidAccountBalanceException : Exception
    {
        public InvalidAccountBalanceException(string message) : base(message) { }
    }
}
