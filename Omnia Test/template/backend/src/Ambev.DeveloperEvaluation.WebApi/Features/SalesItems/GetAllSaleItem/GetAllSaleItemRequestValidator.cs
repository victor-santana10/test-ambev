using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;

public class GetAllSaleItemRequestValidator : AbstractValidator<GetAllSaleItemRequest>
{
    public GetAllSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
