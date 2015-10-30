using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLengthAttribute : ValidationAttribute
    {
        public MaxLengthAttribute(int length)
        {
            if (length <= 0)
            {
                // TODO: resource
                throw new ArgumentException("MaxLengthAttribute must have a Length value that is greater than zero.");
            }

            Length = length;
        }

        public int Length { get; private set; }
    }
}
