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
        newPlayer.Save();
      }
      else
      {
        Player newPlayer = new Player(name, 20, 20, agility, intel, strength, luck);
        newPlayer.Save();
      }

      return RedirectToAction("Index", "Court");
    }

  }
}
