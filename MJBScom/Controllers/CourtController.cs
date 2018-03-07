using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System.Collections.Generic;
using System;

namespace MJBScom.Controllers
{
  public class CourtController : Controller
  {

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
      if (dir == 37) //left
      {
        int x = activePlayer.GetX() - 1;
        if (x < 0) { x = 0; }
        activePlayer.SetX(x);
      }
      else if (dir == 38) //up
      {
        int y = activePlayer.GetY() - 1;
        if (y < 0) { y = 0; }
        activePlayer.SetY(y);
      }
      else if (dir == 39) //right
      {
        int x = activePlayer.GetX() + 1;
        if (x >= 21) { x = 20; }
        activePlayer.SetX(x);
      }
      else if (dir == 40) //down
      {
        int y = activePlayer.GetY() + 1;
        if (y >= 11) { y = 10; }
        activePlayer.SetY(y);
      }
      activePlayer.Update();
      return RedirectToAction("Index");
    }
  }
}
