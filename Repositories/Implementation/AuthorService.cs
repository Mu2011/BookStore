using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Repositories.Implementation;

public class AuthorService(DatabaseContext context) : IAuthorService
{
  private readonly DatabaseContext _context = context;
  public bool Add(Author model)
  {
    try
    {
      _context.Author.Add(model);
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

  public Author FindById(int id) => _context.Author.Find(id);

  public IEnumerable<Author> GetAll() => _context.Author.ToList();


  public bool Update(Author model)
  {
    try
    {
      _context.Author.Update(model);
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