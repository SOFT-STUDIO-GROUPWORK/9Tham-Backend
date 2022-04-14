using Microsoft.EntityFrameworkCore;
using Tham_Backend.Models;

namespace Tham_Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Articles> Articles { get; set; }
    public DbSet<Bloggers> Bloggers { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<Tham_Backend.Models.TagModel> TagModel { get; set; }
}