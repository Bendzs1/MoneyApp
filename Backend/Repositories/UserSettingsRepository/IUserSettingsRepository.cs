using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Repositories;

public interface IUserSettingsRepository
{
    Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate);
    Task<UserSettings?> GetById(int id);
    Task<UserSettings?> GetByUser(User user);
    Task<UserSettings> Add(UserSettings userSettings);
    Task Update(UserSettings userSettings);
    Task Delete(UserSettings userSettings);
    Task SaveChanges();
}