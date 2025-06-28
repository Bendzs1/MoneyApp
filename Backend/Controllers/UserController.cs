using Backend.Dtos;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controller;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUSerService _userService;

    public UserController(IUSerService UserService)
    {
        _userService = UserService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthDto registerDto)
    {
        try
        {
            var user = await _userService.Add(registerDto);
            var token = _userService.GenerateJwtToken(user);
            var response = new AuthResponseDto
            {
                Token = token,
                User = new UserDto(user)
            };
            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDto registerDto)
    {
        try
        {
            var result = await _userService.Verify(registerDto);
            if (result.Item1)
            {
                var token = _userService.GenerateJwtToken(result.Item2);
                var response = new AuthResponseDto
                {
                    Token = token,
                    User = new UserDto(result.Item2)
                };
                return Ok(response);
            }
            else
            {
                throw new ArgumentException("Incorrect password");
            }
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}