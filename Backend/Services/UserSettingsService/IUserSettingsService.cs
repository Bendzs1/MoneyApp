using Backend.Dtos;

namespace Backend.Services;

public interface IUserSettingsService
{
    Task<UserSettings> CreateDefault();
}