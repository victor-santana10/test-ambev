using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.SalesId).NotEmpty().NotNull();
        RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(x => x.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(x => x.Total).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(x => x.Discount).NotNull().PrecisionScale(18, 2, true);
        RuleFor(x => x.IsActive).NotEmpty().NotNull();
    }
}