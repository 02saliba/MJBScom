using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;

namespace MJBScom.Controllers
{
  public class CourtController : Controller
  {

    [HttpGet("/court")]
    public ActionResult Index()
    {
      return View("Index");
    }
  }
}
