using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;

namespace MJBScom.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Intro()
    {
      return View("Intro");
    }

    [HttpGet("/new-game")]
    public ActionResult Index()
    {
      return View("Index");
    }
  }
}
