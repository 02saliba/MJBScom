using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System;
using System.Collections.Generic;

namespace MJBScom.Controllers
{
  public class BattleController : Controller
  {

    [HttpGet("/battle/start")]
    public ActionResult StartBattle()
    {
      Team userTeam = new Team("Cam's Team", 0, true);
      Team enemyTeam = new Team("Other Team", 0, false);

      userTeam.Save();
      enemyTeam.Save();

      Player UserPlayer = new Player("Cameron", 50, 50);
      UserPlayer.Save();
      Player EnemyPlayer = new Player("Not Cameron", 20, 20);
      EnemyPlayer.Save();

      userTeam.AddPlayer(UserPlayer.GetId());
      enemyTeam.AddPlayer(EnemyPlayer.GetId());

      Console.WriteLine(userTeam.GetAllPlayers()[0].GetName());
      Console.WriteLine(enemyTeam.GetAllPlayers()[0].GetName());

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", userTeam);
      model.Add("enemy", enemyTeam);

      return View("Index", model);
    }

    [HttpGet("/battle")]
    public ActionResult Index()
    {
      Player UserPlayer = new Player("Cameron", 50, 50);
      UserPlayer.Save();
      Player EnemyPlayer = new Player("Not Cameron", 20, 20);
      EnemyPlayer.Save();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", UserPlayer);
      model.Add("enemy", EnemyPlayer);

      return View("Index", model);
    }

    [HttpGet("/battle/{attackerId}/attack/{targetId}")]
    public ActionResult Attack(int attackerId, int targetId)
    {
      Player attacker = Player.Find(attackerId);
      Player target = Player.Find(targetId);

      target.SetHPRemaining(target.GetHPRemaining() - attacker.GetStrength());
      target.Update();

      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("user", attacker);
      model.Add("enemy", target);

      return View("Index", model);
    }
  }
}
