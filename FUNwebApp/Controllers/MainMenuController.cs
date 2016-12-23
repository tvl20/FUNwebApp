using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerAppFUN2.DAL;
using KillerFUNwebApp1._0.Models;

namespace FUNwebApp.Controllers
{
    public class MainMenuController : Controller
    {
        private PlayerBizLog repo = new PlayerBizLog(new MSSQLplayerRepo());


        // GET: MainMenu
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.players = repo.GetAllPlayers();
            return View();
        }

        [HttpPost]
        public RedirectResult Index(string selected_character)
        {
            Console.WriteLine(selected_character);
            var chracter = repo.getPlayer(selected_character);
            Session["selected_character"] = chracter;
            return Redirect("/Map/Player");
        }
    }
}