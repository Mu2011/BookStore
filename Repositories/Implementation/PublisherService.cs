using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Repositories.Implementation;

public class PublisherService(DatabaseContext context) : IPublisherService
{
  private readonly DatabaseContext _context = context;
  public bool Add(Publisher model)
  {
    try
    {
      _context.Publisher.Add(model);
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

  public Publisher FindById(int id) => _context.Publisher.Find(id);

  public IEnumerable<Publisher> GetAll() => _context.Publisher.ToList();


  public bool Update(Publisher model)
  {
    try
    {
      _context.Publisher.Update(model);
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