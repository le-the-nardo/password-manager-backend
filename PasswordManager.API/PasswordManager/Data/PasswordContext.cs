using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Data;

public class PasswordContext : DbContext
{
    public PasswordContext(DbContextOptions<PasswordContext> options) : base(options) {}
    
    public DbSet<Password> Passwords { get; set; }
}