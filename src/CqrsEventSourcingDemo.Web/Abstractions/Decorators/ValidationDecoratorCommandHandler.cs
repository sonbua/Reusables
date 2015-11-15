using Reusables.Cqrs;
using Reusables.Validation;

namespace CqrsEventSourcingDemo.Web.Abstractions.Decorators
{
    public class ValidationDecoratorCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private readonly IValidator<TCommand> _validator;
        private readonly ICommandHandler<TCommand> _innerHandler;

        public ValidationDecoratorCommandHandler(IValidator<TCommand> validator, ICommandHandler<TCommand> innerHandler)
        {
            _validator = validator;
            _innerHandler = innerHandler;
        }

        public void Handle(TCommand command)
        {
            foreach (var validationResult in _validator.Validate(command))
            {
                throw new ValidationException(validationResult.ErrorMessage);
            }

            _innerHandler.Handle(command);
        }
    }
}
