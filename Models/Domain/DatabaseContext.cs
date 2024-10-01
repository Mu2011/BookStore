using Microsoft.EntityFrameworkCore;

namespace Mu_BookStore.Models.Domain;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
  public DbSet<Genre> Genre { get; set; }
  public DbSet<Author> Author { get; set; }
  public DbSet<Publisher> Publisher { get; set; }
  public DbSet<Book> Book { get; set; }
}