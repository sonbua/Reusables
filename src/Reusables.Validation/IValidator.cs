using System.Collections.Generic;

namespace Reusables.Validation
{
    public interface IValidator<TInstance>
    {
        IEnumerable<ValidationResult> Validate(TInstance instance);
    }
}
