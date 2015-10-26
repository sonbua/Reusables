using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class StringLengthAttributeValidator : ValidationAttributeValidator<StringLengthAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, StringLengthAttribute attribute)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var num = ((string) value).Length;

            if (num >= attribute.MinimumLength && num <= attribute.MaximumLength)
            {
                return ValidationResult.Success;
            }
            
            if (num < attribute.MinimumLength)
            {
                // TODO: resource
                return new ValidationResult(string.Format("{0} should contain at least {1} characters.", context.MemberName, attribute.MinimumLength), context.MemberName);
            }

            // TODO: resource
            return new ValidationResult(string.Format("{0} should contain at most {1} characters.", context.MemberName, attribute.MaximumLength), context.MemberName);
        }
    }
}
