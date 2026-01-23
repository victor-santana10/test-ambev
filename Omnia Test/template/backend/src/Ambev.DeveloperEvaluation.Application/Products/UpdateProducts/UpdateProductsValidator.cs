using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProducts
{
    internal class UpdateProductsCommandValidator : AbstractValidator<UpdateProductsCommand>
    {
        public UpdateProductsCommandValidator()
        {
            RuleFor(branch => branch.Id).NotEmpty().NotNull();
            RuleFor(product => product.Name).NotEmpty().Length(3, 50);
            RuleFor(product => product.Price).NotEmpty().NotNull().PrecisionScale(18, 2, true).GreaterThan(0);
        }
    }
}
