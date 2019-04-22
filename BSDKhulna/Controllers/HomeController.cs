using BSDKhulna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSDKhulna.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            AdminDBContext db = new AdminDBContext();
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();

            ViewBag.rolelist = new SelectList(context.Roles.ToList(), "Name", "Name");//new SelectList(list1);

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
            AdminDBContext db = new AdminDBContext();
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();
            return View();
        }
    }
}