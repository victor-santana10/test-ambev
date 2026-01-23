using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SalesItems.CreateSaleItem;

public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
{
    public Guid SalesId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}