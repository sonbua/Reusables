namespace Reusables.Validation
{
    public class ValidationResult
    {
        public static readonly ValidationResult Success = null;

        public ValidationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
