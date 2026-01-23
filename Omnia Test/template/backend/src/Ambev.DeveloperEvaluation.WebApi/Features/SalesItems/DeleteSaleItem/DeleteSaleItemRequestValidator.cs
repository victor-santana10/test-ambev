using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.DeleteSaleItem;

public class DeleteSaleItemRequestValidator : AbstractValidator<DeleteSaleItemRequest>
{
    public DeleteSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
