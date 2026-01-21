using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISalesItemsRepository
{
    Task<SalesItems> CreateAsync(SalesItems salesItem, CancellationToken cancellationToken = default);

    Task<SalesItems?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IQueryable<SalesItems>> GetAllBySalesIdAsync(Guid salesId);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SalesItems?> UpdateAsync(SalesItems salesItem, CancellationToken cancellationToken = default);
}
