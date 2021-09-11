using FluentValidation;

namespace SalesTaxes.Domain.Entities.Validation
{
    public class SaleItemValidation : AbstractValidator<SaleItem>
    {
        public SaleItemValidation()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The {PropertyName} must be supplied");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("The {PropertyName} must be supplied")
                .GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("The {PropertyName} must be supplied")
                .GreaterThan(0).WithMessage("The {PropertyName} must be greater than 0");

            RuleFor(x => x.Origin)
                .NotNull().WithMessage("The {PropertyName} must be supplied");

            RuleFor(x => x.Category)
                .NotNull().WithMessage("The {PropertyName} must be supplied");
        }
    }
}