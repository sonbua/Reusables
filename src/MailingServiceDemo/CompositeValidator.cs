using System.Collections.Generic;
using System.Linq;
using Reusables.Validation;

namespace MailingServiceDemo
{
    public class CompositeValidator<T> : IValidator<T>
    {
        private readonly IEnumerable<IValidator<T>> _validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public int Order { get; }

        public IEnumerable<ValidationResult> Validate(T instance)
        {
            return _validators.OrderBy(x => x.Order)
                              .SelectMany(x => x.Validate(instance))
                              .Where(x => x != ValidationResult.Success);
        }
    }
}
