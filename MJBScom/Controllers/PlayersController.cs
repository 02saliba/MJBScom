using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;

namespace MJBScom.Controllers
{
  public class PlayersController : Controller
  {

    [HttpGet("/players/new")]
    public ActionResult CreatePlayerForm()
    {
      return View();
    }

    [HttpPost("/players/new")]
    public ActionResult CreatePlayer()
    {
      Player newPlayer = new Player(Request.Form["name"], 20, 20, 5, 5, 5, 5);
      newPlayer.Save();
      return RedirectToAction("Index", "Court");
    }

  }
}
