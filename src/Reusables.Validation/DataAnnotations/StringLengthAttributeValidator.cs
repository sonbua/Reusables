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
                return new ValidationResult(errorMessage: string.Format("The field {0} must be a string with a minimum length of {1} and a maximum length of {2}.", context.MemberName, attribute.MinimumLength, attribute.MaximumLength),
                                            memberName: context.MemberName);
            }

            // TODO: resource
            return new ValidationResult(errorMessage: string.Format("The field {0} must be a string with a maximum length of {1}.", context.MemberName, attribute.MaximumLength),
                                        memberName: context.MemberName);
        }
    }
}
