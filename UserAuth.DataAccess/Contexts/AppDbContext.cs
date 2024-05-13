using Microsoft.EntityFrameworkCore;
using UserAuth.Domain.Entities;
namespace UserAuth.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
