namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public bool IsActive { get; set; } = true;
    public List<SalesItemsRequest> SalesItems { get; set; }
}

public class SalesItemsRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}