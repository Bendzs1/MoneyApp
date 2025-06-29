using Backend.Dtos;
using Backend.Repositories;

namespace Backend.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRP;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository)
    {
        _userSettingsRP = userSettingsRepository;
    }
    public async Task<UserSettings> CreateDefault()
    {
        var defaultUserSetting = new UserSettings();
        defaultUserSetting.BaseCurrency = Currency.EUR;
        try
        {
            var returnUserSettings = await _userSettingsRP.Add(defaultUserSetting);
            await _userSettingsRP.SaveChanges();
            return returnUserSettings;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Unable to save the user settings.", e);
        }
    }
}