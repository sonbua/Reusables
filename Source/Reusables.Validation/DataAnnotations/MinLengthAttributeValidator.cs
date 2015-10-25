using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class MinLengthAttributeValidator : ValidationAttributeValidator<MinLengthAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, MinLengthAttribute attribute)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var str = value as string;
            var length = str == null ? ((Array) value).Length : str.Length;

            if (length >= attribute.Length)
            {
                return ValidationResult.Success;
            }

            // TODO: resource
            return new ValidationResult(string.Format("{0} should be longer.", context.MemberName));
        }
    }
}
