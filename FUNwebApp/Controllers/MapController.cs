using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KillerFUNwebApp1._0.Models;

namespace FUNwebApp.Controllers
{
    public class MapController : Controller
    {
        public MapController()
        {
            
        }

        // GET: Map
        public ActionResult Map()
        {
            return View();
        }
    }
}