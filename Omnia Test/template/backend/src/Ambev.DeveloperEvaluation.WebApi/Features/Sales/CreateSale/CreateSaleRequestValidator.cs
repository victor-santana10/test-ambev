using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.BranchId).NotEmpty().NotNull();
        RuleFor(x => x.IsActive).NotEmpty().NotNull();

        RuleForEach(x => x.SalesItems)
            .SetValidator(new SalesItemsRequestValidator());
    }
}