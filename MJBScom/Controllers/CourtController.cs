using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System.Collections.Generic;
using System;

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
        enemy.Move(r.Next(2) * 2 + 38);
        enemy.Update();
      }
      
      return RedirectToAction("Index");
    }
  }
}
