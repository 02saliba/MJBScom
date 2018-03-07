using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Routing;

namespace MJBScom.Controllers
{
  public class CourtController : Controller
  {
    private static Random r = new Random();

    [HttpGet("/court")]
    public ActionResult Index()
    {
      List<Player> allPlayers = Player.GetAll();

      return View("Index", allPlayers);
    }

    [HttpGet("/court/{playerId}/{dir}")]
    public ActionResult Move(int dir)
    {
      Player activePlayer = Player.Find(1);
      activePlayer.Move(dir);
      activePlayer.Update();

      foreach(Player enemy in Player.GetEnemies())
      {
        // enemy.Move(r.Next(4) + 37);
        // enemy.Move(r.Next(4) + 37);
        // enemy.Update();
      }
      int enemyInRange = activePlayer.fightDetect();
      if (enemyInRange != -1)
      {
        int getAttackerId = activePlayer.GetId();
        RouteValueDictionary model = new RouteValueDictionary{};
        model.Add("attackerId", getAttackerId);
        model.Add("targetId", enemyInRange);
        return RedirectToAction("Index", "Battle", model);
      }
      else
      {
        return RedirectToAction("Index");
      }
    }
  }
}
