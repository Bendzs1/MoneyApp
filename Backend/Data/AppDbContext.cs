using Microsoft.EntityFrameworkCore.Sqlite;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Provider> Providers => Set<Provider>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<AccountBalance> Balances => Set<AccountBalance>();
}