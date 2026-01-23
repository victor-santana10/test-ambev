using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.UpdateSaleItem
{
    internal class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        public UpdateSaleItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.SalesId).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        }
    }
}
