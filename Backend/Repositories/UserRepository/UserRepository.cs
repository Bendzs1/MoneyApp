using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;
using System.Transactions;
using System.Linq.Expressions;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate)
    {
        return await _context.Users.Where(predicate).ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUserName(string UserName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Update(User user)
    {
        var olduser = await GetById(user.Id);
        if (olduser == null) throw new ArgumentException("User not found");
        if (olduser.UserName != user.UserName)
        {
            var existing = await GetByUserName(user.UserName);
            if (existing != null)
                throw new ArgumentException("User name is already taken.");
        }
        _context.Users.Update(user);
    }

    public async Task Delete(User user)
    {
        var existing = await GetById(user.Id);
        if (existing == null) throw new ArgumentException("User not found");
        existing.Status = false;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}