namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesItems.GetAllSaleItem;

public class GetAllSaleItemResponse
{
    public List<GetSaleItemAllResponse> SalesItems { get; set; }
}

public class GetSaleItemAllResponse
{
    public Guid Id { get; set; }
    public Guid SalesId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
