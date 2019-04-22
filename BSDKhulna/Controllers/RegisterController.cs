using BSDKhulna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BSDKhulna.Controllers
{
    public class RegisterController : Controller
    {

        // GET: Employee
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return View(context.Users.ToList());
        }
        [AllowAnonymous]
        public ActionResult ViewSupplier()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return View(context.Users.ToList());
        }
        //[Authorize(Roles = "admin")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = context.Users.Where(d => d.Id == id).FirstOrDefault();
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var user = context.Users.Where(d => d.Id == id).FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}