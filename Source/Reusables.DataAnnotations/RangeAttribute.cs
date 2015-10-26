using System;

namespace Reusables.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RangeAttribute : ValidationAttribute
    {
        public RangeAttribute(int minimum, int maximum)
        {
            if (minimum > maximum)
            {
                // TODO: resource
                throw new InvalidOperationException("Max must be greater than or equal to min.");
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        public RangeAttribute(double minimum, double maximum)
        {
            if (minimum > maximum)
            {
                // TODO: resource
                throw new InvalidOperationException("Max must be greater than or equal to min.");
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        public object Minimum { get; private set; }

        public object Maximum { get; private set; }
    }
}
