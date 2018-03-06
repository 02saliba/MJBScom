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
        newPlayer.SetX(0);
        newPlayer.SetY(6);
        newPlayer.Save();
      }
      Player enemy1 = new Player("Joe", 20, 20);
      enemy1.SetX(6);
      enemy1.SetY(2);
      Player enemy2 = new Player("Frank", 20, 20);
      enemy2.SetX(12);
      enemy2.SetY(5);
      Player enemy3 = new Player("Cam", 20, 20);
      enemy3.SetX(18);
      enemy3.SetY(7);
      enemy1.Save();
      enemy2.Save();
      enemy3.Save();

      return RedirectToAction("Index", "Court");
    }

  }
}
