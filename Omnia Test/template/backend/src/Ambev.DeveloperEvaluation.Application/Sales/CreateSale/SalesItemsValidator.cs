using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class SalesItemsValidator : AbstractValidator<SalesItemsCommand>
    {
        public SalesItemsValidator()
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
