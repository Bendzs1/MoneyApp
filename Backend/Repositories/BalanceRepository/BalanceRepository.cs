using System.Linq.Expressions;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly AppDbContext _context;

    public BalanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AccountBalance?> GetById(int id)
    {
        return await _context.Balances.FindAsync(id);
    }

    public async Task Add(AccountBalance balance)
    {
        await _context.Balances.AddAsync(balance);
    }

    public async Task Update(AccountBalance balance)
    {
        var existing = await GetById(balance.Id);
        if (existing == null)
            throw new Exception("Balance not found");
        _context.Balances.Update(balance);
    }

    public async Task Delete(AccountBalance balance)
    {
        var existing = await GetById(balance.Id);
        if (existing == null)
            throw new Exception("Balance not found");
        existing.Status = false;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
