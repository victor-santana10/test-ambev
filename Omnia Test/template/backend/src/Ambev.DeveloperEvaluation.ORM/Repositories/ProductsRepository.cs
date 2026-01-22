using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

public class ProductsRepository : IProductsRepository
{
    private readonly DefaultContext _context;

    public ProductsRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Products> CreateAsync(Products product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Products?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Products?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(b => b.Name.Equals(name), cancellationToken);
    }

    public Task<IQueryable<Products>> GetAllAsync()
    {
        return Task.FromResult(_context.Products.OrderBy(b => b.Name).AsQueryable());
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Products?> UpdateAsync(Products product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(b => b.Id.Equals(product.Id), cancellationToken);

        if (existingProduct == null)
            return null;

        product.UpdatedAt = DateTime.UtcNow;
        product.CreatedAt = existingProduct.CreatedAt;
        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync(cancellationToken);
        return existingProduct;
    }

}
