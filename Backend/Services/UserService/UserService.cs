using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dtos;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace Backend.Services;

public class UserService : IUSerService
{
    IUserRepository _userRp;
    IUserSettingsRepository _userSettingsRp;
    PasswordHasher<User> _hasher = new PasswordHasher<User>();
    readonly JwtSettings _jwtsettings;

    public UserService(IUserRepository userRepository, IUserSettingsRepository userSettingsRepository, IOptions<JwtSettings> jwtSettings)
    {
        _userSettingsRp = userSettingsRepository;
        _userRp = userRepository;
        _jwtsettings = jwtSettings.Value;
    }

    public async Task<User> Add(AuthDto dto)
    {
        if (dto.UserName.Length > 49)
            throw new ArgumentException("The username length must be less than 50 characters!");
        if (await _userRp.GetByUserName(dto.UserName) != null)
            throw new ArgumentException("Username is already taken!");

        var userSettingModel = new UserSettings();
        var userSetting = await _userSettingsRp.Add(userSettingModel);
        var user = new User(dto.UserName, userSetting);

        //Only storing the hashed password.
        string hashedPassword = _hasher.HashPassword(user, dto.Password);
        user.Password = hashedPassword;

        await _userRp.Add(user);
        await _userRp.SaveChanges();
        return user;
    }

    public async Task<(bool, User)> Verify(AuthDto dto)
    {
        var user = await _userRp.GetByUserName(dto.UserName);
        if (user == null)
            throw new ArgumentException("User doesn't exist.");
        if (user.Status == false)
            throw new ArgumentException("User is deleted.");

        //Comparing the password hashes.
        var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);

        return (result == PasswordVerificationResult.Success, user);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtsettings.ExpirationMinutes),
            Issuer = _jwtsettings.Issuer,
            Audience = _jwtsettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.Key)),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}