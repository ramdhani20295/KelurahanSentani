using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KelurahanSentani.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            if (User.IsInRole("Administrator"))
                return RedirectToAction("Administrator", "Home");
            else if (User.IsInRole( "Lurah"))
                return RedirectToAction("Lurah", "Home");
            else if (User.IsInRole( "RW"))
                return RedirectToAction("Rw", "Home");
            else if (User.IsInRole("RT"))
                return RedirectToAction("Rt", "Home");
            else
            return View();
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Administrator()
        {
            return View();
        }

        [Authorize(Roles = "RW")]
        public ActionResult rw()
        {
            return View();
        }

        [Authorize(Roles = "RT")]
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