using Backend.Models;

namespace Backend.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; } = null!;
}