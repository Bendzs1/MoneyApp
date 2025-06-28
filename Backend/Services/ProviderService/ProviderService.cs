using Backend.Dtos;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Backend.Services;

public class ProviderService : IProviderService
{
    IProviderRepository _providerRp;
    IAccountRepository _accountRp;

    public ProviderService(IProviderRepository providerRepository, IAccountRepository accountRepository)
    {
        _providerRp = providerRepository;
        _accountRp = accountRepository;
    }

    public async Task<int> AddProvider(Provider provider, User iniator)
    {
        if (provider.User != iniator)
            throw new ArgumentException("The user and iniator doesn't match");

        await _providerRp.Add(provider);
        await _providerRp.SaveChanges();
        return provider.Id;
    }

    public async Task DeleteProvider(int id, User iniator)
    {
        var provider = await CheckAndGetProvider(id, iniator);

        await _providerRp.Delete(provider);
        await _providerRp.SaveChanges();
    }

    public async Task<IEnumerable<int>> GetAccountIds(int id, User iniator)
    {
        var provider = await CheckAndGetProvider(id, iniator);
        return provider.Accounts.Select(a => a.Id);
    }

    //Returns a provider with only the name and summed balance.
    public async Task<ProviderDto> GetProvider(int id, User iniator)
    {
        var provider = await CheckAndGetProvider(id, iniator);
        var providerDto = new ProviderDto();
        providerDto.Name = provider.Name;
        providerDto.SummedBalance = await GetSummedBalance(provider);
        return providerDto;
    }

    public async Task UpdateProvider(Provider provider, User iniator)
    {
        if (provider.User != iniator)
            throw new ArgumentException("The user and iniator doesn't match");
        await _providerRp.Update(provider);
        await _providerRp.SaveChanges();
    }

    public async Task<decimal> GetSummedBalance(Provider provider)
    {
        var balances = await Task.WhenAll(
            provider.Accounts
            .Select(account => _accountRp
            .GetCurrentBalance(account.Id)));

        return balances.Sum(b => b?.Balance ?? 0);
    }

    //Checks if the the provider exists
    //and the sender matches with the owner of the provider.
    private async Task<Provider> CheckAndGetProvider(int id, User iniator)
    {
        var provider = await _providerRp.GetById(id);
        if (provider == null)
            throw new ArgumentException("Provider not found");
        if (provider.User != iniator)
            throw new ArgumentException("The user and iniator doesn't match");
        return provider;
    }
}