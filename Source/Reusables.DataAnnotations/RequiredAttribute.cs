using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// The default value is false.
        /// </summary>
        public bool AllowEmptyString { get; set; }
    }
}
