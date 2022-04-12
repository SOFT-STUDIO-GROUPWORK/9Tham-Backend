using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
}