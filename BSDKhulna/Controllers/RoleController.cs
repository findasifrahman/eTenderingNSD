using BSDKhulna.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSDKhulna.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;
        public RoleController()
        {
            ////////////////////////////////////
            context = new ApplicationDbContext();
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {

            ////////////////////////////////////
            context = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                try
                {
                    context.Roles.Add(Role);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    var db = new ApplicationDbContext();
                    IEnumerable<SelectListItem> basetypes = db.Roles.Select(
                        b => new SelectListItem { Value = b.Name, Text = b.Name });
                    ViewData["basetype"] = basetypes;
                    return View(Role);
                }
            }
            else
            {
                var db = new ApplicationDbContext();
                IEnumerable<SelectListItem> basetypes = db.Roles.Select(
                    b => new SelectListItem { Value = b.Name, Text = b.Name });
                ViewData["basetype"] = basetypes;
                return View(Role);
            }


        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        // POST: LocationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ////////////////////////////////////
            var role = context.Roles.Where(d => d.Name == id).FirstOrDefault();
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}