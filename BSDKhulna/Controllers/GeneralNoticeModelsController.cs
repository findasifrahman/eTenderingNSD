using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSDKhulna.Models;
using System.IO;

namespace BSDKhulna.Controllers
{
    public class GeneralNoticeModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: GeneralNoticeModels
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Index()
        {
            return View(db.GeneralNoticeModels.ToList());
        }
        [AllowAnonymous]
        public ActionResult GeneralNoticeFront()
        {
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();
            return View(db.GeneralNoticeModels.ToList());
        }
        // GET: GeneralNoticeModels/Details/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralNoticeModels generalNoticeModels = db.GeneralNoticeModels.Find(id);
            if (generalNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(generalNoticeModels);
        }

        // GET: GeneralNoticeModels/Create
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralNoticeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DescriptionOfNotice,NoticeDownloadUrl")] GeneralNoticeModels generalNoticeModels, HttpPostedFileBase file1)
        {
            string subPath = "~/GeneralNotice"; // your code goes here
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            subPath = "~/GeneralNotice";

            if (file1 ==null)
            {
                ModelState.AddModelError("", "File is not attached!");
            }

            HttpPostedFileBase file = file1;
            if (ModelState.IsValid)
            {
                ////////////////
                if (file != null)
                {
                    var allowedExtensions = new[] { ".pdf" };
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/GeneralNotice" ), myfile);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        generalNoticeModels.NoticeDownloadUrl = Path.Combine("~/GeneralNotice", myfile);
                        
                        file.SaveAs(path);
                    }
                }
                ///////////////
                db.GeneralNoticeModels.Add(generalNoticeModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generalNoticeModels);
        }

        // GET: GeneralNoticeModels/Edit/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralNoticeModels generalNoticeModels = db.GeneralNoticeModels.Find(id);
            if (generalNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(generalNoticeModels);
        }

        // POST: GeneralNoticeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DescriptionOfNotice,NoticeDownloadUrl")] GeneralNoticeModels generalNoticeModels, HttpPostedFileBase file1)
        {
            HttpPostedFileBase file = file1;

            var Prevpath = Path.Combine(Server.MapPath(db.GeneralNoticeModels.Where(m => m.ID == generalNoticeModels.ID).Select(m => m.NoticeDownloadUrl).FirstOrDefault()));
            generalNoticeModels.NoticeDownloadUrl = db.GeneralNoticeModels.Where(m => m.ID == generalNoticeModels.ID).Select(m => m.NoticeDownloadUrl).FirstOrDefault();
            if (System.IO.File.Exists(Prevpath) && file != null)
            {
                System.IO.File.Delete(Prevpath);
            }

            if (ModelState.IsValid)
            {
                ////////////////
                if (file != null)
                {
                    var allowedExtensions = new[] { ".pdf" };
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/GeneralNotice"), myfile);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        generalNoticeModels.NoticeDownloadUrl = Path.Combine("~/GeneralNotice", myfile);

                        file.SaveAs(path);
                    }
                }
                ///////////////
                db.Entry(generalNoticeModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generalNoticeModels);
        }

        // GET: GeneralNoticeModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralNoticeModels generalNoticeModels = db.GeneralNoticeModels.Find(id);
            if (generalNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(generalNoticeModels);
        }

        // POST: GeneralNoticeModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var path = Path.Combine(Server.MapPath(db.GeneralNoticeModels.Where(m => m.ID == id).Select(m => m.NoticeDownloadUrl).FirstOrDefault()));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            GeneralNoticeModels generalNoticeModels = db.GeneralNoticeModels.Find(id);
            db.GeneralNoticeModels.Remove(generalNoticeModels);
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
