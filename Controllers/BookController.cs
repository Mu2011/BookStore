using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Controllers;

// [Route("Book")]

public class BookController(ILogger<BookController> logger, IBookService bookService,
  IAuthorService authorService, IGenreService genreService, IPublisherService publisherService) : Controller
{
  private readonly ILogger<BookController> _logger = logger;
  private readonly IBookService _bookService = bookService;
  private readonly IAuthorService _authorService = authorService;
  private readonly IGenreService _genreService = genreService;
  private readonly IPublisherService _publisherService = publisherService;


  public IActionResult Add()
  {
    var model = new Book()
    {
      AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList(),
      PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList(),
      GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList()
    };
    return View(model);
  }
  [HttpPost]
  public IActionResult Add(Book model)
  {
    model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
    model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
    model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();

    if (!ModelState.IsValid)
      return View(model);
    var result = _bookService.Add(model);
    if (result)
    {
      TempData["msg"] = "Book added successfully";
      return RedirectToAction(nameof(Add));
    }
    TempData["msg"] = "Error has occured on server side";

    return View(model);
  }

  public IActionResult Update(int id)
  {
    var model = _bookService.FindById(id);
    model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
    model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
    model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
    return View(model);
  }
  [HttpPost]
  public IActionResult Update(Book model)
  {
    model.AuthorList = _authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
    model.PublisherList = _publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
    model.GenreList = _genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
    if (!ModelState.IsValid)
      return View(model);
    var result = _bookService.Update(model);
    if (result)
      return RedirectToAction("GetAll");
    TempData["msg"] = "Error has occured on server side";

    return View(model);
  }

  public IActionResult Delete(int id)
  {
    _bookService.Delete(id);
    return RedirectToAction("GetAll");
  }

  public IActionResult GetAll() => View(_bookService.GetAll());

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View();
}