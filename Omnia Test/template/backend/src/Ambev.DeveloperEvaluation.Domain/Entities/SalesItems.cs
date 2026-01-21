using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SalesItems : BaseEntity
    {
        public Guid SalesId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public void Cancel()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void NotCancel()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
