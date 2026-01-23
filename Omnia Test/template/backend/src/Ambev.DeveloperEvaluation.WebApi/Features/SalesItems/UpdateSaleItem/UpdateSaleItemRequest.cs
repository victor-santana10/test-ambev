namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.UpdateSaleItem;

public class UpdateSaleItemRequest
{
    public Guid Id { get; set; }
    public Guid SalesId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }
}
