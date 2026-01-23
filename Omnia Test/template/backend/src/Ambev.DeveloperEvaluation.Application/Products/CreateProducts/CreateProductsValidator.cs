using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProducts;

public class CreateProductsCommandValidator : AbstractValidator<CreateProductsCommand>
{
    public CreateProductsCommandValidator()
    {
        RuleFor(product => product.Name).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
    }
}