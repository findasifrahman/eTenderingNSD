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
    public class SuplierUploadModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: SuplierUploadModels
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            return View(db.SuplierUploadModels.ToList());
        }

        // GET: SuplierUploadModels/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Find(id);
            if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }
            return View(suplierUploadModels);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ViewUploads(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x => x.LetterID == id).FirstOrDefault();
            /*if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }*/
            return View(db.SuplierUploadModels.Where(x => x.LetterID == id).ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ViewUploadDetails(int? letterid,string supplierid)
        {
            if (letterid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x=> x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();
            if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.pictureURL = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.pictureURL).FirstOrDefault();
            ViewBag.supplierAddress = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.Address).FirstOrDefault();
            ViewBag.supplierCompany = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.CompanyName).FirstOrDefault();
            ViewBag.cell  = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.CellNumber).FirstOrDefault();
            return View(suplierUploadModels);
        }
        [Authorize(Roles = "Supplier")]
        public ActionResult SupplierUploadFrontPage(int? letterid, string supplierid)
        {
            /*if (letterid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x => x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();
            /*if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }*/
            bool vall = true;
            //ViewBag.createshow = true;
            if (suplierUploadModels != null)
            {
                vall = false;
            }

            ViewBag.createshow = vall;
            ViewBag.letetid = letterid;
            ViewBag.suppid = supplierid;

            ViewBag.pictureURL = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.pictureURL).FirstOrDefault();
            ViewBag.supplierAddress = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.Address).FirstOrDefault();
            ViewBag.supplierCompany = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.CompanyName).FirstOrDefault();
            ViewBag.cell = db.SupplierModels.Where(x => x.SupplierName == supplierid).Select(x => x.CellNumber).FirstOrDefault();
            return View(suplierUploadModels);
        }
        [Authorize(Roles = "Supplier")]
        // GET: SuplierUploadModels/Create
        public ActionResult Create(int? letterid, string supplierid)
        {
            var denolist = new List<string>();
            denolist.Add("NO"); denolist.Add("SET"); denolist.Add("L/I"); denolist.Add("LTR");
            denolist.Add("MTR"); denolist.Add("LINE"); denolist.Add("CFT"); denolist.Add("PCS"); denolist.Add("PRS"); denolist.Add("SHT");
            denolist.Add("KG"); denolist.Add("BOX"); denolist.Add("PKT"); denolist.Add("COPY"); denolist.Add("RM"); denolist.Add("Roll");
            denolist.Add("Lbs"); denolist.Add("Bottle"); denolist.Add("Feet"); denolist.Add("Metric Ton"); denolist.Add("Other");
            ViewBag.denolist = new SelectList(denolist);

            ViewBag.let = letterid;
            ViewBag.sup = supplierid;

            ViewBag.details = db.ProcurementAndTenderModels.Where(x => x.ID == letterid).Select(x=> x.ItemDescription).FirstOrDefault();
            ViewBag.quantity = db.ProcurementAndTenderModels.Where(x => x.ID == letterid).Select(x => x.Quantity).FirstOrDefault();

            return View();
        }

        // POST: SuplierUploadModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Supplier")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LetterID,SupplierID,SupplierContact,Quantity,Deno,price,Date,QuoteDocURL,Other1DocURL,Other2DocURL,Other3DocURL,Other4DocURL")] SuplierUploadModels suplierUploadModels, 
            HttpPostedFileBase fileQuoteDoc, HttpPostedFileBase fileOther1Doc, HttpPostedFileBase fileOther2Doc, HttpPostedFileBase fileOther3Doc, HttpPostedFileBase fileOther4Doc)
        {
            var denolist = new List<string>();
            denolist.Add("NO"); denolist.Add("SET"); denolist.Add("L/I"); denolist.Add("LTR");
            denolist.Add("MTR"); denolist.Add("LINE"); denolist.Add("CFT"); denolist.Add("PCS"); denolist.Add("PRS"); denolist.Add("SHT");
            denolist.Add("KG"); denolist.Add("BOX"); denolist.Add("PKT"); denolist.Add("COPY"); denolist.Add("RM"); denolist.Add("Roll");
            denolist.Add("Lbs"); denolist.Add("Bottle"); denolist.Add("Feet"); denolist.Add("Metric Ton"); denolist.Add("Other");
            ViewBag.denolist = new SelectList(denolist);

            ViewBag.let = suplierUploadModels.LetterID;
            ViewBag.sup = suplierUploadModels.SupplierID;

            ViewBag.details = db.ProcurementAndTenderModels.Where(x => x.ID == suplierUploadModels.LetterID).Select(x => x.ItemDescription).FirstOrDefault();
            ViewBag.quantity = db.ProcurementAndTenderModels.Where(x => x.ID == suplierUploadModels.LetterID).Select(x => x.Quantity).FirstOrDefault();
            if (ModelState.IsValid)
            {
                string subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "quote"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileQuoteDoc != null)
                {
                    upload_file(fileQuoteDoc, suplierUploadModels, "quote", suplierUploadModels.LetterID);
                }
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other1"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther1Doc != null)
                {
                    upload_file(fileOther1Doc, suplierUploadModels, "other1", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other2"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther2Doc != null)
                {
                    upload_file(fileOther2Doc, suplierUploadModels, "other2", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other3"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther3Doc != null)
                {
                    upload_file(fileOther3Doc, suplierUploadModels, "other3", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other4"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther4Doc != null)
                {
                    upload_file(fileOther4Doc, suplierUploadModels, "other4", suplierUploadModels.LetterID);
                }
                ////////

                db.SuplierUploadModels.Add(suplierUploadModels);
                db.SaveChanges();
                return RedirectToAction("ProcurementFrontPage", "ProcurementAndTenderModels");
                //return RedirectToAction("Index");
            }

            return View(suplierUploadModels);
        }
        public void upload_file(HttpPostedFileBase file, SuplierUploadModels suplierUploadModels, string type, int LetterNo)
        {
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
                    var path = Path.Combine(Server.MapPath("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type ), myfile);
                    if (type.Contains("quote"))
                    {
                        suplierUploadModels.QuoteDocURL = Path.Combine("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type, myfile);
                    }
                    else if (type.Contains("other1"))
                    {
                        suplierUploadModels.Other1DocURL = Path.Combine("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type, myfile);
                    }
                    else if (type.Contains("other2"))
                    {
                        suplierUploadModels.Other2DocURL = Path.Combine("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type, myfile);
                    }
                    else if (type.Contains("other3"))
                    {
                        suplierUploadModels.Other3DocURL = Path.Combine("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type, myfile);
                    }
                    else if (type.Contains("other4"))
                    {
                        suplierUploadModels.Other4DocURL = Path.Combine("~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + LetterNo.ToString() + "/" + type, myfile);
                    }

                    file.SaveAs(path);
                }
            }
        }
        [Authorize(Roles = "Supplier")]
        // GET: SuplierUploadModels/Edit/5
        public ActionResult Edit(int? letterid,string supplierid)
        {
            if (letterid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var denolist = new List<string>();
            denolist.Add("NO"); denolist.Add("SET"); denolist.Add("L/I"); denolist.Add("LTR");
            denolist.Add("MTR"); denolist.Add("LINE"); denolist.Add("CFT"); denolist.Add("PCS"); denolist.Add("PRS"); denolist.Add("SHT");
            denolist.Add("KG"); denolist.Add("BOX"); denolist.Add("PKT"); denolist.Add("COPY"); denolist.Add("RM"); denolist.Add("Roll");
            denolist.Add("Lbs"); denolist.Add("Bottle"); denolist.Add("Feet"); denolist.Add("Metric Ton"); denolist.Add("Other");
            ViewBag.denolist = new SelectList(denolist);

            ViewBag.let = letterid;
            ViewBag.sup = supplierid;

            ViewBag.details = db.ProcurementAndTenderModels.Where(x => x.ID == letterid).Select(x => x.ItemDescription).FirstOrDefault();
            ViewBag.quantity = db.ProcurementAndTenderModels.Where(x => x.ID == letterid).Select(x => x.Quantity).FirstOrDefault();

            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x => x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();
            if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }
            return View(suplierUploadModels);
        }

        // POST: SuplierUploadModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Supplier")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LetterID,SupplierID,SupplierContact,Quantity,Deno,price,Date,QuoteDocURL,Other1DocURL,Other2DocURL,Other3DocURL,Other4DocURL")] SuplierUploadModels suplierUploadModels,
            HttpPostedFileBase fileQuoteDoc, HttpPostedFileBase fileOther1Doc, HttpPostedFileBase fileOther2Doc, HttpPostedFileBase fileOther3Doc, HttpPostedFileBase fileOther4Doc)
        {
            var denolist = new List<string>();
            denolist.Add("NO"); denolist.Add("SET"); denolist.Add("L/I"); denolist.Add("LTR");
            denolist.Add("MTR"); denolist.Add("LINE"); denolist.Add("CFT"); denolist.Add("PCS"); denolist.Add("PRS"); denolist.Add("SHT");
            denolist.Add("KG"); denolist.Add("BOX"); denolist.Add("PKT"); denolist.Add("COPY"); denolist.Add("RM"); denolist.Add("Roll");
            denolist.Add("Lbs"); denolist.Add("Bottle"); denolist.Add("Feet"); denolist.Add("Metric Ton"); denolist.Add("Other");
            ViewBag.denolist = new SelectList(denolist);

            ViewBag.let = suplierUploadModels.LetterID;
            ViewBag.sup = suplierUploadModels.SupplierID;

            ViewBag.details = db.ProcurementAndTenderModels.Where(x => x.ID == suplierUploadModels.LetterID).Select(x => x.ItemDescription).FirstOrDefault();
            ViewBag.quantity = db.ProcurementAndTenderModels.Where(x => x.ID == suplierUploadModels.LetterID).Select(x => x.Quantity).FirstOrDefault();

            var PrevpathfileQuote = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.QuoteDocURL).FirstOrDefault()));
            suplierUploadModels.QuoteDocURL = db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.QuoteDocURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileQuote) && fileQuoteDoc != null)
            {
                System.IO.File.Delete(PrevpathfileQuote);
            }

            var PrevpathfileOther1 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other1DocURL).FirstOrDefault()));
            suplierUploadModels.Other1DocURL = db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other1DocURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileOther1) && fileOther1Doc != null)
            {
                System.IO.File.Delete(PrevpathfileOther1);
            }

            var PrevpathfileOther2 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other2DocURL).FirstOrDefault()));
            suplierUploadModels.Other2DocURL = db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other2DocURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileOther2) && fileOther2Doc != null)
            {
                System.IO.File.Delete(PrevpathfileOther2);
            }

            var PrevpathfileOther3 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other3DocURL).FirstOrDefault()));
            suplierUploadModels.Other3DocURL = db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other3DocURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileOther3) && fileOther3Doc != null)
            {
                System.IO.File.Delete(PrevpathfileOther3);
            }

            var PrevpathfileOther4 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other4DocURL).FirstOrDefault()));
            suplierUploadModels.Other4DocURL = db.SuplierUploadModels.Where(m => m.SupplierID == suplierUploadModels.SupplierID).Select(m => m.Other4DocURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileOther4) && fileOther4Doc != null)
            {
                System.IO.File.Delete(PrevpathfileOther4);
            }

            if (ModelState.IsValid)
            {
                string subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "quote"; // your code goes here
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileQuoteDoc != null)
                {
                    upload_file(fileQuoteDoc, suplierUploadModels, "quote", suplierUploadModels.LetterID);
                }
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other1"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther1Doc != null)
                {
                    upload_file(fileOther1Doc, suplierUploadModels, "other1", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other2"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther2Doc != null)
                {
                    upload_file(fileOther2Doc, suplierUploadModels, "other2", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other3"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther3Doc != null)
                {
                    upload_file(fileOther3Doc, suplierUploadModels, "other3", suplierUploadModels.LetterID);
                }
                ////////
                ////////
                subPath = "~/SupplierUpload/" + suplierUploadModels.SupplierID + "/" + suplierUploadModels.LetterID.ToString() + "/" + "other4"; // your code goes here
                exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                if (fileOther4Doc != null)
                {
                    upload_file(fileOther4Doc, suplierUploadModels, "other4", suplierUploadModels.LetterID);
                }
                ////////

                db.Entry(suplierUploadModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProcurementFrontPage","ProcurementAndTenderModels");
                //return RedirectToAction("Index");
            }
            return View(suplierUploadModels);
        }
        [Authorize(Roles = "Admin,Supplier")]
        // GET: SuplierUploadModels/Delete/5
        public ActionResult Delete(int? letterid, string supplierid)
        {
            if (letterid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x => x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();
            if (suplierUploadModels == null)
            {
                return HttpNotFound();
            }
            return View(suplierUploadModels);
        }

        // POST: SuplierUploadModels/Delete/5
        [Authorize(Roles = "Admin,Supplier")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? letterid, string supplierid)
        {
            SuplierUploadModels suplierUploadModels = db.SuplierUploadModels.Where(x => x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();

            var Prevpathfile1 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.QuoteDocURL).FirstOrDefault()));
            suplierUploadModels.QuoteDocURL = db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.QuoteDocURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile1))
            {
                System.IO.File.Delete(Prevpathfile1);
            }

            var Prevpathfile2 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other1DocURL).FirstOrDefault()));
            suplierUploadModels.Other1DocURL = db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other1DocURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile2))
            {
                System.IO.File.Delete(Prevpathfile2);
            }


            var Prevpathfile3 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other2DocURL).FirstOrDefault()));
            suplierUploadModels.Other2DocURL = db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other2DocURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile3))
            {
                System.IO.File.Delete(Prevpathfile3);
            }

            var Prevpathfile4 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other3DocURL).FirstOrDefault()));
            suplierUploadModels.Other3DocURL = db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other3DocURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile4))
            {
                System.IO.File.Delete(Prevpathfile4);
            }
            var Prevpathfile5 = Path.Combine(Server.MapPath(db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other4DocURL).FirstOrDefault()));
            suplierUploadModels.Other4DocURL = db.SuplierUploadModels.Where(m => m.ID == suplierUploadModels.ID).Select(m => m.Other4DocURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile5))
            {
                System.IO.File.Delete(Prevpathfile5);
            }

            //////////////
            SuplierUploadModels suplierUploadModels1 = db.SuplierUploadModels.Where(x => x.LetterID == letterid && x.SupplierID == supplierid).FirstOrDefault();
            db.SuplierUploadModels.Remove(suplierUploadModels1);
            db.SaveChanges();
            return RedirectToAction("ProcurementFrontPage", "ProcurementAndTenderModels");
            //return RedirectToAction("Index");
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
