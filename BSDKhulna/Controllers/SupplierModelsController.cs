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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BSDKhulna.Controllers
{
    public class SupplierModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: SupplierModels
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ////////////
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "SupplierName" ? "SupplierName_asc" : "SupplierName";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = from s in db.SupplierModels select s;
            DateTime dt = DateTime.Today.Date;
            try
            {
                dt = Convert.ToDateTime(searchString);
            }
            catch { }
            int ff = 0;
            try
            {
                ff = Convert.ToInt32(searchString);
            }
            catch { }
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.SupplierName.Contains(searchString) || s.CompanyName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "SupplierName_asc":
                    students = students.OrderBy(s => s.SupplierName);
                    break;
                case "SupplierName":
                    students = students.OrderByDescending(s => s.SupplierName);
                    break;
                default:  // Name ascending 
                    students = students.OrderByDescending(s => s.ID);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            /////////////////////////////////////////////////////////
            return View(students.ToPagedList(pageNumber, pageSize));//db.AccountVoucherModels.ToList());

            //return View(db.SupplierModels.ToList());
        }
        [AllowAnonymous]
        public ActionResult ViewSupplier()
        {
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();
            return View(db.SupplierModels.ToList());
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: SupplierModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierModels supplierModels = db.SupplierModels.Find(id);
            if (supplierModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: SupplierModels/Create
        public ActionResult Create()
        {
            ViewBag.CompanyNamelist = new SelectList(db.CompanyNameModels.ToList(), "CompanyName", "CompanyName");
            ViewBag.SupplierGrouplist = new SelectList(db.SupplierGroupModels.ToList(), "GroupName", "GroupName");
            return View();
        }

        // POST: SupplierModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierName,ID,SupplierGroup,CompanyName,PhoneNumber,CellNumber,Email,password,BarCodeNumber,IDCardNumber,pictureURL,Published,Address")] SupplierModels supplierModels, HttpPostedFileBase filepicture)
        {

            ViewBag.CompanyNamelist = new SelectList(db.CompanyNameModels.ToList(), "CompanyName", "CompanyName");
            ViewBag.SupplierGrouplist = new SelectList(db.SupplierGroupModels.ToList(), "GroupName", "GroupName");
            if (ModelState.IsValid)
            {

                string subPath = "~/SupplierPic/" + supplierModels.SupplierName.ToString(); // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (filepicture != null)
                {
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                    var fileName = Path.GetFileName(filepicture.FileName);
                    var ext = Path.GetExtension(filepicture.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = supplierModels.SupplierName + ext; //appending the name with id  
                                                                           // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/SupplierPic/" + supplierModels.SupplierName), myfile);

                        supplierModels.pictureURL = Path.Combine("~/SupplierPic/" + supplierModels.SupplierName, myfile);
                        filepicture.SaveAs(path);
                    }
                }
                ////////////
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!RoleManager.RoleExists("Supplier"))
                {
                    var roleresult = RoleManager.Create(new IdentityRole("Supplier"));
                }
                
                //Create User=Admin with password=123456
                //var user = new ApplicationUser();
                var user = new ApplicationUser { UserName = supplierModels.SupplierName, Email = supplierModels.Email, roleName = "Supplier" };
                //user.UserName = "gazi mupu";//supplierModels.SupplierName;
                //user.Email = supplierModels.Email;
                //user.roleName = "Supplier";
                var adminresult = UserManager.Create(user, supplierModels.password);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Supplier");
                }
                AddErrors(adminresult);
                /////////////////////////
                try
                {
                    db.SupplierModels.Add(supplierModels);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.InnerException.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2627)
                    {
                        ModelState.AddModelError("", "Supplier already registered!");

                    }
                    return View(supplierModels);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(supplierModels);
                }
                ////////////
                /////////////
                return RedirectToAction("Index");
            }

            return View(supplierModels);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: SupplierModels/Edit/5
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Edit(string id)
        {
            ViewBag.CompanyNamelist = new SelectList(db.CompanyNameModels.ToList(), "CompanyName", "CompanyName");
            ViewBag.SupplierGrouplist = new SelectList(db.SupplierGroupModels.ToList(), "GroupName", "GroupName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierModels supplierModels = db.SupplierModels.Find(id);
            if (supplierModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierModels);
        }

        // POST: SupplierModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierName,SupplierGroup,CompanyName,PhoneNumber,CellNumber,Email,password,BarCodeNumber,IDCardNumber,pictureURL,Published,Address")] SupplierModels supplierModels, HttpPostedFileBase filepicture)
        {
            ViewBag.CompanyNamelist = new SelectList(db.CompanyNameModels.ToList(), "CompanyName", "CompanyName");
            ViewBag.SupplierGrouplist = new SelectList(db.SupplierGroupModels.ToList(), "GroupName", "GroupName");
            if (ModelState.IsValid)
            {

                var Prevpath = Path.Combine(Server.MapPath(db.SupplierModels.Where(m => m.SupplierName == supplierModels.SupplierName).Select(m => m.pictureURL).FirstOrDefault()));
                supplierModels.pictureURL = db.SupplierModels.Where(m => m.SupplierName == supplierModels.SupplierName).Select(m => m.pictureURL).FirstOrDefault();
                if (System.IO.File.Exists(Prevpath) && filepicture != null)
                {
                    System.IO.File.Delete(Prevpath);
                }

                if (filepicture != null)
                {
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                    var fileName = Path.GetFileName(filepicture.FileName);
                    var ext = Path.GetExtension(filepicture.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = supplierModels.SupplierName + ext; //appending the name with id  
                                                                           // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/SupplierPic/" + supplierModels.SupplierName), myfile);

                        supplierModels.pictureURL = Path.Combine("~/SupplierPic/" + supplierModels.SupplierName, myfile);
                        filepicture.SaveAs(path);
                    }
                }
                try
                {
                    db.Entry(supplierModels).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.InnerException.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2627)
                    {
                        ModelState.AddModelError("", "Supplier already registered!");
                        return View(supplierModels);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(supplierModels);
                }
                //////////////
                /////////////
                return RedirectToAction("Index");
            }
            return View(supplierModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: SupplierModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierModels supplierModels = db.SupplierModels.Find(id);
            if (supplierModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierModels);
        }

        // POST: SupplierModels/Delete/5
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SupplierModels supplierModels = db.SupplierModels.Find(id);
            try
            {
                var path = Path.Combine(Server.MapPath(db.SupplierModels.Where(m => m.SupplierName == id).Select(m => m.pictureURL).FirstOrDefault()));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                db.SupplierModels.Remove(supplierModels);
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("", "Supplier already registered!");
                    return View(supplierModels);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message.ToString());
                return View(supplierModels);
            }
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
