using System.Linq.Expressions;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly AppDbContext _context;

    public ProviderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Provider>> GetAll(Expression<Func<Provider, bool>> predicate)
    {
        return await _context.Providers
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<Provider?> GetById(int id)
    {
        return await _context.Providers.FindAsync(id);
    }

    public async Task Add(Provider provider)
    {
        await _context.Providers.AddAsync(provider);
    }

    public async Task Update(Provider provider)
    {
        var existing = await GetById(provider.Id);
        if (existing == null)
            throw new Exception("Provider not found");
        _context.Providers.Update(provider);
    }

    public async Task Delete(Provider provider)
    {
        var existing = await GetById(provider.Id);
        if (existing == null)
            throw new Exception("Provider not found");

        existing.Status = false; // Soft delete
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
