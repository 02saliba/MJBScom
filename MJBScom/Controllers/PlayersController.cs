using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System;

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
      string name = Request.Form["name"];
      int agility;
      int intel;
      int strength;
      int luck;
      if (!int.TryParse(Request.Form["agility"], out agility)) agility=0;
      if (!int.TryParse(Request.Form["intel"], out intel)) intel=0;
      if (!int.TryParse(Request.Form["strength"], out strength)) strength=0;
      if (!int.TryParse(Request.Form["luck"], out luck)) luck=0;


      if(agility == 0 && intel == 0 && strength == 0 && luck == 0)
      {
        Player newPlayer = new Player(name, 20, 20);
        newPlayer.SetAllegience(true);
        newPlayer.Save();
      }
      else
      {
        Player newPlayer = new Player(name, 20, 20, agility, intel, strength, luck);
        newPlayer.SetAllegience(true);
        newPlayer.Save();
      }
      Player enemy1 = new Player("Joe", 20, 20);
      Player enemy2 = new Player("Frank", 20, 20);
      Player enemy3 = new Player("Cam", 20, 20);
      enemy1.Save();
      enemy2.Save();
      enemy3.Save();

      return RedirectToAction("Index", "Court");
    }

  }
}
