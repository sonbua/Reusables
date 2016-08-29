using Reusables.Diagnostics.Contracts;

namespace Reusables.Validation
{
    public class ValidationResult
    {
        public static readonly ValidationResult Success = null;

        public ValidationResult(string errorMessage, string memberName)
        {
            Requires.IsNotNull(errorMessage, nameof(errorMessage));
            Requires.IsNotNull(memberName, nameof(memberName));

            ErrorMessage = errorMessage;
            MemberName = memberName;
        }

        public string ErrorMessage { get; }

        public string MemberName { get; }
    }
}
