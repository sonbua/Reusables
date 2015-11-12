using System.Collections.Generic;

namespace Reusables.Validation
{
    public class NullValidator<TInstance> : IValidator<TInstance>
    {
        public int Order { get; }

        public IEnumerable<ValidationResult> Validate(TInstance instance)
        {
            yield break;
        }
    }
}
