using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;

namespace MJBScom.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View("Index", "Hello World");
    }
  }
}
