using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Repositories;

interface IBalanceRepository
{
    Task<AccountBalance?> GetById(int id);
    Task Add(AccountBalance balance);
    Task Update(AccountBalance balance);
    Task Delete(AccountBalance balance);
    Task SaveChanges();
}