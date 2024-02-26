using FluentValidation;
using NMHAssignment.WebAPI.Models;

namespace NMHAssignment.WebAPI.Validators
{
    public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
    {
        public CalculationRequestValidator()
        {
            RuleFor(c => c.Key).GreaterThan(0).WithMessage("Route parameter must be greater than zero.");
            RuleFor(c => c.Body.Input).NotNull().WithMessage("Request must contain body with field \"input\"");
            RuleFor(c => c.Body.Input).GreaterThan(0).WithMessage("Input must be positive real number");
        }
    }
}
