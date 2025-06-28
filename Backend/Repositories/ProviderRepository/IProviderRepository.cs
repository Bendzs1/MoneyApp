using System.Linq.Expressions;
using Backend.Models;

namespace Backend.Repositories;

public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetAll(Expression<Func<Provider, bool>> predicate);
    Task<Provider?> GetById(int id);
    Task Add(Provider provider);
    Task Update(Provider provider);
    Task Delete(Provider provider);
    Task SaveChanges();
}