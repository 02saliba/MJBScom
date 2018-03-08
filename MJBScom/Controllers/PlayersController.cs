using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System;
using System.Collections.Generic;

namespace MJBScom.Controllers
{
  public class PlayersController : Controller
  {

    [HttpGet("/players/new")]
    public ActionResult CreatePlayerForm()
    {
      Player.DeleteAll();
      BattleText.DeleteAll();
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
      Player enemy1 = new Player("Kayla", 20, 20);
      enemy1.SetX(6);
      enemy1.SetY(2);
      Player enemy2 = new Player("Frank", 20, 20);
      enemy2.SetX(12);
      enemy2.SetY(5);
      Player enemy3 = new Player("Cam", 20, 20);
      enemy3.SetX(18);
      enemy3.SetY(7);
      Player enemy4 = new Player("Nico", 20, 20);
      enemy4.SetX(8);
      enemy4.SetY(2);
      Player enemy5 = new Player("Johnny", 20, 20);
      enemy5.SetX(16);
      enemy5.SetY(4);

      enemy1.Save();
      enemy2.Save();
      enemy3.Save();
      enemy4.Save();
      enemy5.Save();

      BattleText t1 = new BattleText(
        "This is gonna be a SLAM DUNK!",
        "Im just gettin warmed up!",
        "Im sorry Michael. Ive let you down..."
      );
      t1.Save();

      BattleText t2 = new BattleText(
        "I dont know if I can beat you, but Ill do my best!",
        "This isnt going well...",
        "Well at least I tried."
      );
      t2.Save();

      BattleText t3 = new BattleText(
        "Check out these sweet moves!!",
        "Im a second half player...",
        "* walks away angrily *"
      );
      t3.Save();

      Random rnd = new Random();
      List<int> texts = BattleText.GetIds();
      enemy1.SetFlavorId(texts[rnd.Next(texts.Count)]);
      enemy2.SetFlavorId(texts[rnd.Next(texts.Count)]);
      enemy3.SetFlavorId(texts[rnd.Next(texts.Count)]);
      enemy4.SetFlavorId(texts[rnd.Next(texts.Count)]);
      enemy5.SetFlavorId(texts[rnd.Next(texts.Count)]);

      return RedirectToAction("Index", "Court");
    }

  }
}
