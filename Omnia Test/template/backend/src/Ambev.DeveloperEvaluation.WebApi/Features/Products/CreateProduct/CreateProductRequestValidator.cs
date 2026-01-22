using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(branch => branch.Name).NotEmpty().Length(3, 50);
        RuleFor(branch => branch.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        RuleFor(branch => branch.IsActive).NotEmpty().NotNull();
    }
}