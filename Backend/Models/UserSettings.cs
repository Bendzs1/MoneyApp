using Backend.Models;
using Microsoft.EntityFrameworkCore;
public class UserSettings
{
    public int Id { get; set; }
    public Currency BaseCurrency { get; set; }
}