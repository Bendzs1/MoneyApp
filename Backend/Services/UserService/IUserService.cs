using Backend.Dtos;
using Backend.Models;

namespace Backend.Services;

public interface IUSerService
{
    Task<User> Add(AuthDto dto);
    Task<(bool, User)> Verify(AuthDto dto);
    string GenerateJwtToken(User user);
}