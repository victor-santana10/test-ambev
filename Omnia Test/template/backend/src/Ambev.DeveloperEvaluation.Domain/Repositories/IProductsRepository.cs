using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductsRepository
{
    Task<Products> CreateAsync(Products product, CancellationToken cancellationToken = default);

    Task<Products?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Products?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IQueryable<Products>> GetAllAsync();

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Products?> UpdateAsync(Products product, CancellationToken cancellationToken = default);
}
