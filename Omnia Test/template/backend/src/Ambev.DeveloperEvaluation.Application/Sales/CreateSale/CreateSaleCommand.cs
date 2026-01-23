using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }

    public List<SalesItemsCommand> SalesItems { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void GroupSalesItems()
    {
        SalesItems = SalesItems
            .GroupBy(x => x.ProductId)
            .Select(g =>
            {
                var totalQuantity = g.Sum(x => x.Quantity);

                if (totalQuantity > 20)
                    throw new InvalidOperationException(
                        $"Não e permitido mais de 20 itens. ProductId: {g.Key}");

                var first = g.First();

                return new SalesItemsCommand
                {
                    ProductId = g.Key,
                    Quantity = totalQuantity,
                    Price = first.Price,
                    Discount = g.Sum(x => x.Discount),
                    Total = g.Sum(x => x.Total),
                    IsActive = true
                };
            })
            .ToList();
    }
}

public class SalesItemsCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }
}