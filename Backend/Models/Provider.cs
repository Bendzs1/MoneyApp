namespace Backend.Models;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Account> Accounts;

    public Provider(bool Status = true)
    {
        this.Status = Status;
    }

}