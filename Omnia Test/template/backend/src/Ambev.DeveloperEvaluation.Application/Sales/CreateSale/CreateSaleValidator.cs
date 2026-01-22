using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.BranchId).NotEmpty().NotNull();
        RuleFor(x => x.Total).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(x => x.IsActive).NotEmpty();
    }
}