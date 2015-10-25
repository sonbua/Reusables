namespace Reusables.DataAnnotations
{
    public class StringLengthAttribute : ValidationAttribute
    {
        public StringLengthAttribute(int maximumLength)
        {
            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public int MinimumLength { get; set; }
    }
}
