namespace BankingSolution.Domain.Exceptions
{
    public class DuplicateIbanException : Exception
    {
        public DuplicateIbanException(string message) :base(message) { }
    }
}
