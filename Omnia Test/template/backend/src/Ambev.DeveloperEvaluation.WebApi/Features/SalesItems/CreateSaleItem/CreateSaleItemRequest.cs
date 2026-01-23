namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.CreateSaleItem;

public class CreateSaleItemRequest
{
    public Guid SalesId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}