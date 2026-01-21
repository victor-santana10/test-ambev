using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

public class SalesRepository : ISalesRepository
{
    private readonly DefaultContext _context;

    public SalesRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sales> CreateAsync(Sales sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<IQueryable<Sales>> GetAllAsync()
    {
        return Task.FromResult(_context.Sales.Where(b => b.IsActive));
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Sales?> UpdateAsync(Sales sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
            .FirstOrDefaultAsync(b => b.Id.Equals(sale.Id), cancellationToken);

        if (existingSale == null)
            return null;

        sale.UpdatedAt = DateTime.UtcNow;
        _context.Entry(existingSale).CurrentValues.SetValues(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return existingSale;
    }
}
