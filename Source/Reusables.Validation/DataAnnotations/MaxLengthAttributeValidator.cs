using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class MaxLengthAttributeValidator : ValidationAttributeValidator<MaxLengthAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, MaxLengthAttribute attribute)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var str = value as string;
            var length = str == null ? ((Array) value).Length : str.Length;

            if (length <= attribute.Length)
            {
                return ValidationResult.Success;
            }

            // TODO: resource
            return new ValidationResult(string.Format("{0} exceeds max length.", context.MemberName));
        }
    }
}
