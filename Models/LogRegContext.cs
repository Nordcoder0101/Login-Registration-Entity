using Microsoft.EntityFrameworkCore;

namespace LogReg.Models
{
  public class LogRegContext : DbContext
  {
    public LogRegContext(DbContextOptions<LogRegContext> options) : base(options) { }
    public DbSet<User> User {get;set;}
  }
}