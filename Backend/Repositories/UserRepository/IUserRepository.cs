using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate);
    Task<User?> GetById(int id);
    Task<User?> GetByUserName(string UserName);
    Task Add(User user);
    Task Update(User user);
    Task Delete(User user);
    Task SaveChanges();
}