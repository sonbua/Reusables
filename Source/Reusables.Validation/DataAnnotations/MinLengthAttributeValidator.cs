using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class MinLengthAttributeValidator : ValidationAttributeValidator<MinLengthAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, MinLengthAttribute attribute)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

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
            return new ValidationResult(string.Format("The field {0} must be a string or array type with a minimum length of '{1}'.", context.MemberName, attribute.Length), context.MemberName);
        }
    }
}
