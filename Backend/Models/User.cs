using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Index(nameof(UserName), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? Password { get; set; }
    public bool Status { get; set; }
    public ICollection<Provider> Providers { get; set; }

    public int UserSettingsId { get; set; }
    public UserSettings UserSettings { get; set; }

    public User()
    {
        Providers = new List<Provider>();
    }

    public User(string userName, UserSettings userSettings, bool status = true) : this()
    {
        this.UserName = userName;
        this.Status = status;
        this.UserSettings = userSettings;
    }
}