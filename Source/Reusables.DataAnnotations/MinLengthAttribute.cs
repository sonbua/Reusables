using System;

namespace Reusables.DataAnnotations
{
    public class MinLengthAttribute : ValidationAttribute
    {
        public MinLengthAttribute(int length)
        {
            if (length < 0)
            {
                // TODO: resource
                throw new ArgumentException("Length must be greater than or equal to 0.");
            }

            Length = length;
        }

        public int Length { get; set; }
    }
}
