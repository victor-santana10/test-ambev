using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetSaleItem;

public class GetSaleItemRequestValidator : AbstractValidator<GetSaleItemRequest>
{
    public GetSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
