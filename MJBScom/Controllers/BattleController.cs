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

      battleMsg.Add(target.GetName() + ": " + BattleText.Find(target.GetFlavorId()).GetStart());

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
        if (Player.GetEnemies().Count == 1 && target.GetName() == "Michael Jordan")
        {
            target.Delete();
            return View("Win");
        }

        battleMsg.Add(target.GetName() + ": " + BattleText.Find(target.GetFlavorId()).GetEnd());
        target.Delete();
        return View("EndBattle", model);
      }

      battleMsg.Add(Player.RandomAttack(target, attacker));

      if (attacker.GetHPRemaining() <= 0)
      {
        Player.DeleteAll();
        return View("Lose");
      }

      return View("Index", model);
    }

    [HttpGet("/battle/stat/{id}/{statId}")]
    public ActionResult UpdateStat(int id, int statId)
    {
      Player foundPlayer = Player.Find(id);
      if (statId == 0) foundPlayer.SetLuck(foundPlayer.GetLuck() + 1);
      else if (statId == 1) foundPlayer.SetAgility(foundPlayer.GetAgility() + 1);
      else if (statId == 2) foundPlayer.SetIntelligence(foundPlayer.GetIntelligence() + 1);
      else if (statId == 3) foundPlayer.SetStrength(foundPlayer.GetStrength() + 1);
      foundPlayer.Update();

      if (Player.GetEnemies().Count == 0)
      {
          Player michaelJordan = new Player("Michael Jordan", 50, 50);
          michaelJordan.Save();

          Dictionary<string, object> mjmodel = new Dictionary<string, object>();
          mjmodel.Add("user", foundPlayer);
          mjmodel.Add("enemy", michaelJordan);
          return View("Final", mjmodel);
      }

      return RedirectToAction("Index", "Court");
    }
  }
}
