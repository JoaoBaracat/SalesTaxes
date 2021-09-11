using FluentValidation;
using FluentValidation.Results;
using SalesTaxes.Domain.Entities;
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

        protected bool Validate<TValidator, TEntity>(TValidator validator, TEntity entity)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validationResult = validator.Validate(entity);

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