using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

public class SalesItemsRepository : ISalesItemsRepository
{
    private readonly DefaultContext _context;

    public SalesItemsRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<SalesItems> CreateAsync(SalesItems salesItem, CancellationToken cancellationToken = default)
    {
        await _context.SalesItems.AddAsync(salesItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return salesItem;
    }

    public async Task<SalesItems?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SalesItems
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<IQueryable<SalesItems>> GetAllBySalesIdAsync(Guid salesId)
    {
        return Task.FromResult(_context.SalesItems.Where(b => b.SalesId.Equals(salesId)));
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var salesItem = await GetByIdAsync(id, cancellationToken);
        if (salesItem == null)
            return false;

        _context.SalesItems.Remove(salesItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<SalesItems?> UpdateAsync(SalesItems salesItem, CancellationToken cancellationToken = default)
    {
        var existingSalesItem = await _context.SalesItems
            .FirstOrDefaultAsync(b => b.Id == salesItem.Id, cancellationToken);

        if (existingSalesItem == null)
            return null;

        salesItem.UpdatedAt = DateTime.UtcNow;
        salesItem.CreatedAt = existingSalesItem.CreatedAt;
        _context.Entry(existingSalesItem).CurrentValues.SetValues(salesItem);
        await _context.SaveChangesAsync(cancellationToken);

        return existingSalesItem;
    }
}
