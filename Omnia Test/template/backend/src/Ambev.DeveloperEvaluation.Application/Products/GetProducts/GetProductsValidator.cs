using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsCommand>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
