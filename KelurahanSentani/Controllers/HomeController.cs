using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KelurahanSentani.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Administrator()
        {
            return View();
        }

        [Authorize(Roles = "Rw")]
        public ActionResult rw()
        {
            return View();
        }

        [Authorize(Roles = "Rt")]
        public ActionResult rt()
        {
            return View();
        }

        [Authorize(Roles = "Lurah")]
        public ActionResult Lurah()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}