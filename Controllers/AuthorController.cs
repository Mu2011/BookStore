using Microsoft.AspNetCore.Mvc;
using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Controllers;

// [Route("Author")]

public class AuthorController(ILogger<AuthorController> logger, IAuthorService service) : Controller
{
  private readonly ILogger<AuthorController> _logger = logger;
  private readonly IAuthorService _service = service;

  public IActionResult Add() => View();
  [HttpPost]
  public IActionResult Add(Author model)
  {
    if (!ModelState.IsValid)
      return View(model);
    var result = _service.Add(model);
    if (result)
    {
      TempData["msg"] = "Author added successfully";
      return RedirectToAction(nameof(Add));
    }
    TempData["msg"] = "Error has occured on server side";

    return View(model);
  }

  public IActionResult Update(int id) => View(_service.FindById(id));
  [HttpPost]
  public IActionResult Update(Author model)
  {
    if (!ModelState.IsValid)
      return View(model);
    var result = _service.Update(model);
    if (result)
      return RedirectToAction("GetAll");
    TempData["msg"] = "Error has occured on server side";

    return View(model);
  }

  public IActionResult Delete(int id)
  {
    _service.Delete(id);
    return RedirectToAction("GetAll");
  }

  public IActionResult GetAll() => View(_service.GetAll());

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View();
}