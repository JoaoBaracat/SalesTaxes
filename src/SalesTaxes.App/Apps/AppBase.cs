using FluentValidation;
using FluentValidation.Results;
using SalesTaxes.Domain.ValueObjects;
using SalesTaxes.Domain.Notifications;
using System.Linq;

namespace SalesTaxes.App.Apps
{
    public abstract class AppBase
    {
        private readonly INotifier _notifier;

        public AppBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool Validate<TValidator, TValueObject>(TValidator validator, TValueObject valueObject)
            where TValidator : AbstractValidator<TValueObject>
            where TValueObject : ValueObject
        {
            var validationResult = validator.Validate(valueObject);

            Notify(validationResult);

            return validationResult.IsValid;
        }

        protected void Notify(ValidationResult validationReult)
        {
            validationReult.Errors.ToList().ForEach((e) => { Notify(e.ErrorMessage); });
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}