using Microsoft.EntityFrameworkCore;

namespace Tham_Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Articles> Articles { get; set; }
}