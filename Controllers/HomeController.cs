using Microsoft.AspNetCore.Mvc;

namespace Mu_BookStore.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
  private readonly ILogger<HomeController> _logger = logger;

  public IActionResult Index() => View();

  public IActionResult Privacy() => View();

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View();
}
