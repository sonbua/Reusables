using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MinLengthAttribute : ValidationAttribute
    {
        public MinLengthAttribute(int length)
        {
            if (length < 0)
            {
                // TODO: resource
                throw new ArgumentException("MinLengthAttribute must have a Length value that is zero or greater.");
            }

            Length = length;
        }

        public int Length { get; set; }
    }
}
