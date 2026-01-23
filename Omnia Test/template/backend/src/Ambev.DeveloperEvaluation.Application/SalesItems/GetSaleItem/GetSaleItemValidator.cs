using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.GetSaleItem;

public class GetSaleItemValidator : AbstractValidator<GetSaleItemCommand>
{
    public GetSaleItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
