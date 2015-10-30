using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class RequiredAttributeValidator : ValidationAttributeValidator<RequiredAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, RequiredAttribute attribute)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (value == null)
            {
                // TODO: resource
                return new ValidationResult(string.Format("The {0} field is required.", context.MemberName), context.MemberName);
            }

            var str = value as string;

            if (str == null || attribute.AllowEmptyString || str.Trim().Length > 0)
            {
                return ValidationResult.Success;
            }

            // TODO: resource
            return new ValidationResult(string.Format("The {0} field is required.", context.MemberName), context.MemberName);
        }
    }
}
