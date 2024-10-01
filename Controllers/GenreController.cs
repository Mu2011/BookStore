using Microsoft.AspNetCore.Mvc;
using Mu_BookStore.Models.Domain;
using Mu_BookStore.Repositories.Abstract;

namespace Mu_BookStore.Controllers;

// [Route("Genre")]
public class GenreController(ILogger<GenreController> logger, IGenreService service) : Controller
{
  private readonly ILogger<GenreController> _logger = logger;
  private readonly IGenreService _service = service;

  public IActionResult Add() => View();
  [HttpPost]
  public IActionResult Add(Genre model)
  {
    if (!ModelState.IsValid)
      return View(model);
    var result = _service.Add(model);
    if (result)
    {
      TempData["msg"] = "Genre added successfully";
      return RedirectToAction(nameof(Add));
    }
    TempData["msg"] = "Error has occured on server side";

    return View(model);
  }

  public IActionResult Update(int id) => View(_service.FindById(id));
  [HttpPost]
  public IActionResult Update(Genre model)
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