using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerAppFUN2.DAL;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

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

        [HttpGet]
        public ActionResult EditCharacters()
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

        public ActionResult DeleteCharacter(string to_delete_character)
        {
            if (string.IsNullOrEmpty(to_delete_character))
            {
                return Redirect("/MainMenu/EditCharacters");
            }
            repo.deletePlayer(to_delete_character);
            return Redirect("/");
        }

        public ActionResult AddCharacter(string character_name, string character_class)
        {
            if (!string.IsNullOrWhiteSpace(character_name) && !string.IsNullOrWhiteSpace(character_class))
            {
                if (!repo.playerNameTaken(character_name))
                {
                    switch (character_class)
                    {
                        case "Knight":
                            repo.addPlayer(character_name, Class.Knight);
                            break;
                        case "Barbarian":
                            repo.addPlayer(character_name, Class.Barbarian);
                            break;
                        case "Crusader":
                            repo.addPlayer(character_name, Class.Crusader);
                            break;
                        case "Paladin":
                            repo.addPlayer(character_name, Class.Paladin);
                            break;
                    }
                    return Redirect("/");
                }
            }
            return Redirect("/MainMenu/EditCharacters");
        }
    }
}