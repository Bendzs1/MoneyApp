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

    public User()
    {
        Providers = new List<Provider>();
    }

    public User(string UserName, bool Status = true) : this()
    {
        this.UserName = UserName;
        this.Status = Status;
    }
}