namespace Reusables.DataAnnotations
{
    public class RequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// The default value is false.
        /// </summary>
        public bool AllowEmptyString { get; set; }
    }
}
