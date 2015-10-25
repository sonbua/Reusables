namespace Reusables.DataAnnotations
{
    public class RegularExpressionAttribute : ValidationAttribute
    {
        public RegularExpressionAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; private set; }
    }
}
