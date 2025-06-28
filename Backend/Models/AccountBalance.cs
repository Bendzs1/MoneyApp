namespace Backend.Models;

public class AccountBalance
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Balance { get; set; }
    public bool Status { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public AccountBalance(bool Status = true)
    {
        this.Status = true;
    }
}