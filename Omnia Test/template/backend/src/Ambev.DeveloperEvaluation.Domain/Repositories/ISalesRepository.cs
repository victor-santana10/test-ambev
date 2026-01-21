using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISalesRepository
{
    Task<Sales> CreateAsync(Sales sale, CancellationToken cancellationToken = default);

    Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IQueryable<Sales>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sales?> UpdateAsync(Sales sale, CancellationToken cancellationToken = default);
}
