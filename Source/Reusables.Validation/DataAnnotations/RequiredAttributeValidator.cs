using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class RequiredAttributeValidator : ValidationAttributeValidator<RequiredAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, RequiredAttribute attribute)
        {
            if (value == null)
            {
                // TODO: resource
                return new ValidationResult(string.Format("{0} cannot be null.", context.MemberName), context.MemberName);
            }

            var str = value as string;

            if (str == null || attribute.AllowEmptyString)
            {
                return ValidationResult.Success;
            }

            if (str.Trim().Length > 0)
            {
                return ValidationResult.Success;
            }

            // TODO: resource
            return new ValidationResult(string.Format("{0} is required.", context.MemberName), context.MemberName);
        }
    }
}
