using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProducts;

public class DeleteProductsValidator : AbstractValidator<DeleteProductsCommand>
{
    public DeleteProductsValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
