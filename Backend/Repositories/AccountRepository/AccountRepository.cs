using System.Linq.Expressions;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetById(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task Add(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task Update(Account account)
    {
        var existing = await GetById(account.Id);
        if (existing == null)
            throw new Exception("Account not found");
        _context.Update(account);
    }

    public async Task Delete(Account account)
    {
        var existing = await GetById(account.Id);
        if (existing == null)
            throw new Exception("Account not found");

        // Soft delete
        existing.Status = false;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<AccountBalance?> GetCurrentBalance(int accountId)
    {
        return await _context.Balances
            .Where(b => b.AccountId == accountId && b.Status == true)
            .OrderByDescending(b => b.Date)
            .FirstOrDefaultAsync();
    }
}
