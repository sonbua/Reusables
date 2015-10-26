using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringLengthAttribute : ValidationAttribute
    {
        private int _minimumLength;

        public StringLengthAttribute(int maximumLength)
        {
            if (maximumLength < 0)
            {
                throw new ArgumentException("Maximum length should be greater than or equal to 0.");
            }

            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public int MinimumLength
        {
            get { return _minimumLength; }
            set
            {
                if (value > MaximumLength)
                {
                    throw new ArgumentException("Minimum length should be less than or equal to maximum length.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("Minimum length should be greater than or equal to 0.");
                }

                _minimumLength = value;
            }
        }
    }
}
