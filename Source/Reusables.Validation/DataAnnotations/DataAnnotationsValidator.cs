using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reusables.DataAnnotations;

namespace Reusables.Validation.DataAnnotations
{
    public class DataAnnotationsValidator<TInstance> : IValidator<TInstance>
    {
        private readonly IServiceProvider _serviceProvider;

        public DataAnnotationsValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<ValidationResult> Validate(TInstance instance)
        {
            return from propertyInfo in typeof (TInstance).GetProperties(BindingFlags.Public)
                   let value = propertyInfo.GetValue(instance)
                   let context = new ValidationContext(propertyInfo.PropertyType, propertyInfo.Name)
                   from attribute in propertyInfo.GetCustomAttributes<ValidationAttribute>().OrderBy(x => x.Order)
                   let validator = ResolveValidatorFor(attribute)
                   select validator.Validate(value, context, attribute);
        }

        private IValidationAttributeValidator ResolveValidatorFor(ValidationAttribute attribute)
        {
            var validatorType = typeof (IValidationAttributeValidator<>).MakeGenericType(attribute.GetType());

            return (IValidationAttributeValidator) _serviceProvider.GetService(validatorType);
        }
    }
}
