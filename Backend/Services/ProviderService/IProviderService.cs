using Backend.Dtos;
using Backend.Models;

namespace Backend.Services;

interface IProviderService
{
    Task<int> AddProvider(Provider provider, User iniator);
    Task UpdateProvider(Provider provider, User iniator);
    Task DeleteProvider(int id, User iniator);
    Task<IEnumerable<int>> GetAccountIds(int id, User iniator);
    Task<ProviderDto> GetProvider(int id, User iniator);
}