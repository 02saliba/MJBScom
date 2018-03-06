using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;

namespace MJBScom.Controllers
{
  public class BattleController : Controller
  {

    [HttpGet("/battle")]
    public ActionResult Index()
    {
      Player UserPlayer;
      Player EnemyPlayer;

      return View("Index", "Hello World");
    }
  }
}
