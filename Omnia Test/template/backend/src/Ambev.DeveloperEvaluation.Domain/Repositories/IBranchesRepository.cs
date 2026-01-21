using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchesRepository
{
    Task<Branches> CreateAsync(Branches branch, CancellationToken cancellationToken = default);

    Task<Branches?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Branches?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IQueryable<Branches>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Branches?> UpdateAsync(Branches branch, CancellationToken cancellationToken = default);
}
