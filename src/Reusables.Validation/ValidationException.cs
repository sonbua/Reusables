using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        private ValidationResult _validationResult;

        public ValidationException(ValidationResult validationResult, ValidationAttribute validatingAttribute, object value) : this(validationResult.ErrorMessage, validatingAttribute, value)
        {
            _validationResult = validationResult;
        }

        public ValidationException(string errorMessage, ValidationAttribute validatingAttribute, object value) : base(errorMessage)
        {
            Value = value;
            ValidationAttribute = validatingAttribute;
        }

        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationAttribute ValidationAttribute { get; private set; }

        public ValidationResult ValidationResult => _validationResult ?? (_validationResult = new ValidationResult(Message, string.Empty));

        public object Value { get; private set; }
    }
}
