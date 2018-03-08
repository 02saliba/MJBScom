using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace MJBScom.Controllers
{
  public class BattleController : Controller
  {

    [HttpGet("/battle/{attackerId}/{targetId}")]
    public ActionResult Index(int attackerId, int targetId)
    {
      Player attacker = Player.Find(attackerId);
      Player target = Player.Find(targetId);

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", attacker);
      model.Add("enemy", target);

      List<string> battleMsg = new List<string>();
      model.Add("msg", battleMsg);

      if (target.GetAgility() > attacker.GetAgility())
      {
        battleMsg.Add(Player.RandomAttack(target, attacker));
      }

      if (attacker.GetHPRemaining() <= 0)
      {
        Player.DeleteAll();
        return RedirectToAction("Index", "Home");;
      }
      return View("Index", model);

    }

    [HttpGet("/battle/{attackerId}/attack/{targetId}/{attackMove}")]
    public ActionResult Attack(int attackerId, int targetId, int attackMove)
    {
      Player attacker = Player.Find(attackerId);
      Player target = Player.Find(targetId);

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", attacker);
      model.Add("enemy", target);

      List<string> battleMsg = new List<string>();
      model.Add("msg", battleMsg);

      string attackMsg = "";
      if (attackMove == 0) attackMsg = Player.AttackShoot(attacker, target);
      else if (attackMove == 1) attackMsg = Player.AttackTimeOut(attacker);
      else if (attackMove == 2) attackMsg = Player.AttackDunk(attacker, target);
      else if (attackMove == 3) attackMsg = Player.AttackZoneDefense(attacker, target);

      battleMsg.Add(attackMsg);

      if (target.GetHPRemaining() <= 0)
      {
        if (Player.GetEnemies().Count == 1 && target.GetName() != "Michael Jordan")
        {
            target.Delete();
            Player michaelJordan = new Player("Michael Jordan", 50, 50);
            michaelJordan.Save();

            Dictionary<string, object> mjmodel = new Dictionary<string, object>();
            mjmodel.Add("user", attacker);
            mjmodel.Add("enemy", michaelJordan);
            return View("Final", mjmodel);
        }
        else if (Player.GetEnemies().Count == 1 && target.GetName() == "Michael Jordan")
        {
            target.Delete();
            return View("Win");
        }
        target.Delete();
        return RedirectToAction("Index", "Court");
      }

      battleMsg.Add(Player.RandomAttack(target, attacker));

      if (attacker.GetHPRemaining() <= 0)
      {
        Player.DeleteAll();
        return View("Lose");
      }



      return View("Index", model);
    }
  }
}
