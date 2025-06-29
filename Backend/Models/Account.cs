namespace Backend.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public int ProviderId { get; set; }
    public Provider Provider { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<AccountBalance> Balances;

    public Account(bool Status = true)
    {
        this.Status = Status;
    }
}