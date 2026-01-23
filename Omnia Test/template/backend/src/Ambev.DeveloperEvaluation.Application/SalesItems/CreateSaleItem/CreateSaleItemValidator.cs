using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
    {
        RuleFor(x => x.SalesId).NotEmpty().NotNull();
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
        RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(x => x.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(x => x.Discount).NotNull().PrecisionScale(18, 2, true);
        RuleFor(x => x.Total).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(x => x.IsActive).NotEmpty();
    }
}