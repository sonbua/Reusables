using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public interface IValidationAttributeValidator
    {
        ValidationResult Validate(object value, ValidationContext context, ValidationAttribute attribute);
    }

    public interface IValidationAttributeValidator<TAttribute> : IValidationAttributeValidator where TAttribute : ValidationAttribute
    {
        ValidationResult Validate(object value, ValidationContext context, TAttribute attribute);
    }
}
