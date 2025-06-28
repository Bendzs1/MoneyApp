using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetById(int id);
    Task<AccountBalance?> GetCurrentBalance(int id);
    Task Add(Account account);
    Task Update(Account account);
    Task Delete(Account account);
    Task SaveChanges();
}