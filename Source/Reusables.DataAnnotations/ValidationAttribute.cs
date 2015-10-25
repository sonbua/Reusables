using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class ValidationAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
