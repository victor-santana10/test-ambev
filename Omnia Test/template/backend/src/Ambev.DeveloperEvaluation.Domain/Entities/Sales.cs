using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sales : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
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
