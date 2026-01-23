using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.DeleteSaleItem;

public class DeleteSaleItemValidator : AbstractValidator<DeleteSaleItemCommand>
{
    public DeleteSaleItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
