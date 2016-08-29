using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Reusables.DataAnnotations;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Validation.DataAnnotations
{
    public class RegularExpressionAttributeValidator : ValidationAttributeValidator<RegularExpressionAttribute>
    {
        public override ValidationResult Validate(object value, ValidationContext context, RegularExpressionAttribute attribute)
        {
            Requires.IsNotNull(context, nameof(context));

            var input = Convert.ToString(value, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(input))
                return ValidationResult.Success;

            var regex = new Regex(attribute.Pattern);
            var match = regex.Match(input);

            if (match.Success && match.Index == 0 && match.Length == input.Length)
                return ValidationResult.Success;

            // TODO: resource
            return new ValidationResult($"The field {context.MemberName} must match the regular expression '{attribute.Pattern}'.", context.MemberName);
        }
    }
}
