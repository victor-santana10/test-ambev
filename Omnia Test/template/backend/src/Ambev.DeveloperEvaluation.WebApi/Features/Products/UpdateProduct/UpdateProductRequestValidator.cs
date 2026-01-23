using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(branch => branch.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(branch => branch.Name).NotEmpty().Length(3, 50);
        RuleFor(branch => branch.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
    }
}
