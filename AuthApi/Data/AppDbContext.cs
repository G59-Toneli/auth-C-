using Microsoft.EntityFrameworkCore;
using AuthApi.Entities;

namespace AuthApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

   public DbSet<User> Users {get; set; }
}



