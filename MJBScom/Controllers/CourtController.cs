using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Routing;

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
      activePlayer.Move(dir);
      activePlayer.Update();
      if (activePlayer.fightDetect() != -1)
      {
        int getTargetId = activePlayer.fightDetect();
        int getAttackerId = activePlayer.GetId();
        RouteValueDictionary model = new RouteValueDictionary{};
        model.Add("attackerId", getAttackerId);
        model.Add("targetId", getTargetId);
        return RedirectToAction("Index", "Battle", model);
      }
      else
      {
        return RedirectToAction("Index");
      }
    }
  }
}
