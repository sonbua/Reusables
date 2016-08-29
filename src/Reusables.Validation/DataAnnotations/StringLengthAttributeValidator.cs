using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class StringLengthAttributeValidator : ValidationAttributeValidator<StringLengthAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, StringLengthAttribute attribute)
        {
            if (value == null)
                return ValidationResult.Success;

            var num = ((string) value).Length;

            if (num >= attribute.MinimumLength && num <= attribute.MaximumLength)
                return ValidationResult.Success;

            if (num < attribute.MinimumLength)
                // TODO: resource
                return new ValidationResult(errorMessage: $"The field {context.MemberName} must be a string with a minimum length of {attribute.MinimumLength} and a maximum length of {attribute.MaximumLength}.",
                                            memberName: context.MemberName);

            // TODO: resource
            return new ValidationResult(errorMessage: $"The field {context.MemberName} must be a string with a maximum length of {attribute.MaximumLength}.",
                                        memberName: context.MemberName);
        }
    }
}
