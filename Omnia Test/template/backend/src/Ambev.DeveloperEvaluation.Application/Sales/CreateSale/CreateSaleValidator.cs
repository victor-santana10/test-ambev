using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.BranchId).NotEmpty().NotNull();
        RuleFor(x => x.IsActive).NotEmpty().NotNull();

        RuleForEach(x => x.SalesItems)
            .SetValidator(new SalesItemsValidator());
    }
}