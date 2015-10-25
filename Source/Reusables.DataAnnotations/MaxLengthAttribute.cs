using System;

namespace Reusables.DataAnnotations
{
    public class MaxLengthAttribute : ValidationAttribute
    {
        public MaxLengthAttribute(int length)
        {
            if (length <= 0)
            {
                // TODO: resource
                throw new ArgumentException("Length must be greater than 0.");
            }

            Length = length;
        }

        public int Length { get; private set; }
    }
}
