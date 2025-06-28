using Backend.Models;

namespace Backend.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public ICollection<int> ProviderIds { get; set; }

    public UserDto(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        ProviderIds = user.Providers.Select(p => p.Id).ToList();
    }
}