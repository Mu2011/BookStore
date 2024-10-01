using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Repositories.Implementation;

public class BookService(DatabaseContext context) : IBookService
{
  private readonly DatabaseContext _context = context;
  public bool Add(Book model)
  {
    try
    {
      _context.Book.Add(model);
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

  public Book FindById(int id) => _context.Book.Find(id);

  public IEnumerable<Book> GetAll()
  {
    var data = (from book in _context.Book
                join author in _context.Author on book.AuthorId equals author.Id
                join publisher in _context.Publisher on book.PublisherId equals publisher.Id
                join genre in _context.Genre on book.GenreId equals genre.Id
                select new Book
                {
                  Id = book.Id,
                  AuthorId = book.AuthorId,
                  GenreId = book.GenreId,
                  Isbn = book.Isbn,
                  PublisherId = book.PublisherId,
                  Title = book.Title,
                  TotalPages = book.TotalPages,
                  GenreName = genre.Name,
                  AuthorName = author.AuthorName,
                  PublisherName = publisher.PublisherName
                }).ToList();
    return data;
  }


  public bool Update(Book model)
  {
    try
    {
      _context.Book.Update(model);
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