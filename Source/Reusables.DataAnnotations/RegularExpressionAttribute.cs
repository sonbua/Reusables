using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RegularExpressionAttribute : ValidationAttribute
    {
        public RegularExpressionAttribute(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                // TODO: resource
                throw new ArgumentException("The pattern must be set to a valid regular expression.");
            }

            Pattern = pattern;
        }

        public string Pattern { get; private set; }
    }
}
