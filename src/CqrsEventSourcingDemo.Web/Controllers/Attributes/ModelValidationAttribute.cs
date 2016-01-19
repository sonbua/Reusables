using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Reusables.Validation;
using Reusables.Validation.DataAnnotations;
using Reusables.Web.Mvc5;
using FilterAttribute = Reusables.Web.Mvc5.FilterAttribute;

namespace CqrsEventSourcingDemo.Web.Controllers.Attributes
{
    public class ModelValidationAttribute : FilterAttribute
    {
    }

    public class ValidateModel : IActionFilter<ModelValidationAttribute>
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidateModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int Order => 0;

        public bool SkipNextFilters => false;

        public void OnActionExecuting(ModelValidationAttribute attribute, ActionExecutingContext filterContext)
        {
            var actionParameters = filterContext.ActionParameters;

            if (actionParameters.Count == 0)
            {
                return;
            }

            var model = actionParameters.Select(x => x.Value).FirstOrDefault(x => x.GetType().IsClass);

            if (model == null)
            {
                return;
            }

            var modelValidatorType = typeof (DataAnnotationsValidator<>).MakeGenericType(model.GetType());

            dynamic validator = _serviceProvider.GetService(modelValidatorType);

            var validationResults = ((IEnumerable<ValidationResult>) validator.Validate((dynamic) model)).Where(x => x != ValidationResult.Success);

            foreach (var result in validationResults)
            {
                filterContext.Controller.ViewData.ModelState.AddModelError(result.MemberName, result.ErrorMessage);
            }
        }

        public void OnActionExecuted(ModelValidationAttribute attribute, ActionExecutedContext filterContext)
        {
            // do nothing
        }
    }
}
