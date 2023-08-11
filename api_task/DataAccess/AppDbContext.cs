using api_task.Models;
using Microsoft.EntityFrameworkCore;

namespace api_task.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User>? Users { get; set; }
}