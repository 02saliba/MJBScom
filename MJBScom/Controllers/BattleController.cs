using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System;
using System.Collections.Generic;

namespace MJBScom.Controllers
{
  public class BattleController : Controller
  {

    [HttpGet("/battle/{attackerId}/{targetId}")]
    public ActionResult Index(int attackerId, int targetId)
    {
      Console.WriteLine("I am in the battle controller");
      Player attacker = Player.Find(attackerId);
      Player target = Player.Find(targetId);

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", attacker);
      model.Add("enemy", target);

      model.Add("userAttacked", false);
      if (target.GetAgility() > attacker.GetAgility())
      {
        attacker.SetHPRemaining(attacker.GetHPRemaining() - target.GetStrength());
        attacker.Update();
        model.Add("enemyAttacked", true);
        model.Add("enemyDamage", target.GetStrength());
      }
      else{
        model.Add("enemyAttacked", false);
      }

      if (attacker.GetHPRemaining() <= 0)
      {
        Player.DeleteAll();
        return RedirectToAction("Index", "Home");;
      }
      return View("Index", model);

    }

    [HttpGet("/battle/{attackerId}/attack/{targetId}")]
    public ActionResult Attack(int attackerId, int targetId)
    {
      Player attacker = Player.Find(attackerId);
      Player target = Player.Find(targetId);

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", attacker);
      model.Add("enemy", target);
      model.Add("enemyDamage", target.GetStrength());
      model.Add("userDamage", attacker.GetStrength());

      model.Add("userAttacked", true);
      model.Add("enemyAttacked", true);

      target.SetHPRemaining(target.GetHPRemaining() - attacker.GetStrength());
      target.Update();

      if (target.GetHPRemaining() <= 0)
      {
        target.Delete();
        if (Player.GetAll().Count == 1)
        {
          Player.DeleteAll();
          return View("Win");
        }
        return RedirectToAction("Index", "Court");
      }

      attacker.SetHPRemaining(attacker.GetHPRemaining() - target.GetStrength());
      attacker.Update();

      if (attacker.GetHPRemaining() <= 0)
      {
        Player.DeleteAll();
        return RedirectToAction("Index", "Home");
      }

      return View("Index", model);
    }
  }
}
