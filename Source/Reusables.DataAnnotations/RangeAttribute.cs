namespace Reusables.DataAnnotations
{
    public class RangeAttribute : ValidationAttribute
    {
        public RangeAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public int Minimum { get; private set; }

        public int Maximum { get; private set; }
    }
}
