using Mu_BookStore.Models.Domain;

namespace Mu_BookStore.Repositories.Abstract;

public interface IAuthorService
{
  bool Add(Author model);
  bool Update(Author model);
  bool Delete(int id);
  Author FindById(int id);
  IEnumerable<Author> GetAll();
}