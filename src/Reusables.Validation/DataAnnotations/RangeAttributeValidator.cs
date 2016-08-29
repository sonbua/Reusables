using System;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class RangeAttributeValidator : ValidationAttributeValidator<RangeAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, RangeAttribute attribute)
        {
            if (value == null)
                return ValidationResult.Success;

            var type = value.GetType();

            if (!typeof (IComparable).IsAssignableFrom(type))
                throw new InvalidOperationException("Cannot compare this value to");

            var comparable = (IComparable) value;

            if (comparable.CompareTo(attribute.Minimum) >= 0 && comparable.CompareTo(attribute.Maximum) <= 0)
                return ValidationResult.Success;

            // TODO: resource
            return new ValidationResult("Invalid", context.MemberName);
        }
    }
}
