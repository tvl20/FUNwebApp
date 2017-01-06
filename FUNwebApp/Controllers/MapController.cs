using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNwebApp.Models.DAL;
using KillerAppFUN2.DAL;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;
using Newtonsoft.Json;

namespace FUNwebApp.Controllers
{
    public class MapController : Controller
    {
        private PlayerBizLog playerRepo = new PlayerBizLog(new MSSQLplayerRepo());
        private RoomBizLog roomRepo = new RoomBizLog(new MSSQLroomRepo());

        // GET: Map
        [HttpGet]
        public ActionResult Player()
        {
            if (Session["selected_character"] != null)
            {
                Player p = (Player)Session["selected_character"];
                return View(p);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Player(string btnPress)
        {
            if (btnPress == "Back to character select")
            {
                return Redirect("/");
            }else if (btnPress == "To the map")
            {
                Player currentPlayer = Session["selected_character"] as Player;
                Session["current_room"] = roomRepo.GetRoom(currentPlayer.CurrentRoomID);
            }
            return Redirect("/Map/Game");
        }

        [HttpGet]
        public ActionResult Game()
        {
            return View();
        }

        public ActionResult GetRoom()
        {
            var room = (Room)Session["current_room"];
            return Json(room, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlayer()
        {
            var player = (Player)Session["selected_character"];
            return Json(player, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddPlayerXP(int xp)
        {
            var player = (Player)Session["selected_character"];
            player.XP += xp;
            Session["selected_character"] = player;
            return Json(player, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePlayerHealth(int hp)
        {
            var player = (Player)Session["selected_character"];
            player.Health = hp;
            Session["selected_character"] = player;
            return Json(player, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LvlUp(string stat)
        {
            if (stat == "max_hp")
            {
                KillerFUNwebApp1._0.Models.Player p = Session["selected_character"] as Player;
                p.levelUp(Stat.MaxHealth);
                Session["selected_character"] = p;
            }
            else if (stat == "attack")
            {
                KillerFUNwebApp1._0.Models.Player p = Session["selected_character"] as Player;
                p.levelUp(Stat.Attack);
                Session["selected_character"] = p;
            }
            else if (stat == "defence")
            {
                KillerFUNwebApp1._0.Models.Player p = Session["selected_character"] as Player;
                p.levelUp(Stat.Defence);
                Session["selected_character"] = p;
            }

            return Redirect("/Map/Player");
        }

        public ActionResult NewWeapon(int weaponID)
        {
            KillerFUNwebApp1._0.Models.Player player = Session["selected_character"] as Player;
            player.PickUpWeapon(weaponID);
            Session["selected_character"] = player;
            return Json(player.CurrentWeapon, JsonRequestBehavior.AllowGet);
        }
    }
}