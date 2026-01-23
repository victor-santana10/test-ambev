using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class SalesItemsRequestValidator : AbstractValidator<SalesItemsRequest>
    {
        public SalesItemsRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull().NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero (0).")
                .LessThanOrEqualTo(20).WithMessage("Não é permitido vender uma quantidade maior que 20 deste item.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Valor do produto deve ser maior que zero (0).");

        }
    }
}
