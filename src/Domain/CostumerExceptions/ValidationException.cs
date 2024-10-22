namespace Domain.CostumerExceptions
{
    public class ValidationException(IEnumerable<string> errorMessages) : Exception
    {
        public IEnumerable<string> ErrorMessages { get; } = errorMessages;
    }


}
