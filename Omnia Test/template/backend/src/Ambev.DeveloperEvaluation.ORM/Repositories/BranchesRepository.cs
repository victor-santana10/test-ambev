using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

public class BranchesRepository : IBranchesRepository
{
    private readonly DefaultContext _context;

    public BranchesRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Branches> CreateAsync(Branches branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    public async Task<Branches?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Branches?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(b => b.Name == name, cancellationToken);
    }

    public Task<IQueryable<Branches>> GetAllAsync()
    {
        return Task.FromResult(_context.Branches.OrderBy(b => b.Name).AsQueryable());
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Branches?> UpdateAsync(Branches branch, CancellationToken cancellationToken = default)
    {
        var existingBranch = await _context.Branches
                    .FirstOrDefaultAsync(x => x.Id.Equals(branch.Id), cancellationToken);

        if (existingBranch == null)
            return null;

        branch.UpdatedAt = DateTime.UtcNow;
        branch.CreatedAt = existingBranch.CreatedAt;
        _context.Entry(existingBranch).CurrentValues.SetValues(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return existingBranch;
    }
}
