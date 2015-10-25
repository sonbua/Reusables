using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public abstract class ValidationAttributeValidator<TAttribute> : IValidationAttributeValidator<TAttribute> where TAttribute : ValidationAttribute
    {
        public ValidationResult Validate(object value, ValidationContext context, ValidationAttribute attribute)
        {
            return Validate(value, context, (TAttribute) attribute);
        }

        public abstract ValidationResult Validate(object value, ValidationContext context, TAttribute attribute);
    }
}
