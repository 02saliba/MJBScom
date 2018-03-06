using Microsoft.AspNetCore.Mvc;
using MJBScom.Models;
using System.Collections.Generic;

namespace MJBScom.Controllers
{
  public class CourtController : Controller
  {

    [HttpGet("/court")]
    public ActionResult Index()
    {
      //create and place enemy
      List<Player> allPlayers = Player.GetAll();

      return View("Index", allPlayers);
    }
  }
}
