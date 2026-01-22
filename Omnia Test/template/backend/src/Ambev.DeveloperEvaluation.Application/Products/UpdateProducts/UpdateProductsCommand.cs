using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProducts;

public class UpdateProductsCommand : IRequest<UpdateProductsResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public decimal Price { get; set; }
    public bool IsActive { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateProductsCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
