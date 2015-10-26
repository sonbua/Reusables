using System;

namespace Reusables.Validation
{
    public class ValidationResult
    {
        public static readonly ValidationResult Success = null;

        public ValidationResult(string errorMessage, string memberName)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }

            if (memberName == null)
            {
                throw new ArgumentNullException("memberName");
            }
            
            ErrorMessage = errorMessage;
            MemberName = memberName;
        }

        public string ErrorMessage { get; private set; }

        public string MemberName { get; private set; }
    }
}
