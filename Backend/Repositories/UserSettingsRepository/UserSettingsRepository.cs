using System.Linq.Expressions;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly AppDbContext _context;

    public UserSettingsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserSettings> Add(UserSettings userSettings)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(UserSettings userSettings)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<UserSettings?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserSettings?> GetByUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task Update(UserSettings userSettings)
    {
        throw new NotImplementedException();
    }
}