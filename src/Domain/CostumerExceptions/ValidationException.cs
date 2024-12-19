namespace Domain.CostumerExceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; }

        public ValidationException(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
