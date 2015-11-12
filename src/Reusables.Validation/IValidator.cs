using System.Collections.Generic;

namespace Reusables.Validation
{
    public interface IValidator<TInstance>
    {
        int Order { get; }

        IEnumerable<ValidationResult> Validate(TInstance instance);
    }
}
