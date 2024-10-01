using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Repositories.Implementation;

public class GenreService(DatabaseContext context) : IGenreService
{
  private readonly DatabaseContext _context = context;
  public bool Add(Genre model)
  {
    try
    {
      _context.Genre.Add(model);
      _context.SaveChanges();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error adding book: {ex.Message}");
      return false;
    }
  }

  public bool Delete(int id)
  {
    try
    {
      var data = FindById(id);
      if (data is null)
        return false;

      _context.Remove(data);
      _context.SaveChanges();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error deleting book: {ex.Message}");
      return false;
    }
  }

  public Genre FindById(int id) => _context.Genre.Find(id);

  public IEnumerable<Genre> GetAll() => _context.Genre.ToList();


  public bool Update(Genre model)
  {
    try
    {
      _context.Genre.Update(model);
      _context.SaveChanges();
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error adding book: {ex.Message}");
      return false;
    }
  }
}