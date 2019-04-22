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
using System.Data.SqlClient;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
namespace BSDKhulna.Controllers
{
    public class ProcurementAndTenderModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: ProcurementAndTenderModels
        [Authorize(Roles = "Admin,IT,CST,LP,CR,CINS,BILL")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "LetterNo" ? "LetterNo_asc" : "LetterNo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = from s in db.ProcurementAndTenderModels select s;
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
                students = students.Where(s => s.ID == ff || s.LetterNo.Contains(searchString) || s.TenderNo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "LetterNo_asc":
                    students = students.OrderBy(s => s.LetterNo);
                    break;
                case "LetterNo":
                    students = students.OrderByDescending(s => s.LetterNo);
                    break;
                default:  // Name ascending 
                    students = students.OrderByDescending(s => s.LetterNo);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            /////////////////////////////////////////////////////////

            //ViewBag.customerList = new SelectList(settingsDBContext.CustomerInfoModels.ToList(), "CustomerName", "CustomerName");
            return View(students.OrderByDescending(x=> x.ITDate).ToPagedList(pageNumber, pageSize));//db.AccountVoucherModels.ToList());


            //return View(db.ProcurementAndTenderModels.ToList());
        }
        public ActionResult SupplierUpload()
        {
            return View();
        }
        [Authorize(Roles = "Admin,IT,CST,LP,CR,CINS,BILL")]
        public ActionResult IndexByDate(string dt1, string dt2)
        {
            ViewBag.from = "From: " + dt1;
            ViewBag.to = "To: " + dt2;


            DateTime tt1 = Convert.ToDateTime(dt1);
            DateTime tt2 = Convert.ToDateTime(dt2);
            //ViewBag.amount = "Total Amount: " + (db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(tt2)).Sum(x => (int?)x.TotalAmount) ?? 0).ToString();
            var mod = db.ProcurementAndTenderModels.Where(x => DbFunctions.TruncateTime(x.ITDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.ITDate) <= DbFunctions.TruncateTime(tt2));
            int tot = mod.Count();

            int? page = 1;
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View("Index", mod.OrderBy(x=> x.ITDate).ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin,IT,LP")]
        public ActionResult CSTApprovalIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var list1 = new List<string>();
            list1.Add("DNS");
            list1.Add("DNST");
            list1.Add("DTS");
            list1.Add("Other");
            ViewBag.departmentlist = list1;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "TenderNo" ? "TenderNo_asc" : "TenderNo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = from s in db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo == null && x.PurchaseOrderNo == null) select s;
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
                students = students.Where(s => s.TenderNo.Contains(searchString) || s.LetterNo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "TenderNo_asc":
                    students = students.OrderBy(s => s.TenderNo);
                    break;
                case "TenderNo":
                    students = students.OrderByDescending(s => s.TenderNo);
                    break;
                default:  // Name ascending 
                    students = students.OrderByDescending(s => s.ID);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            /////////////////////////////////////////////////////////

            //ViewBag.customerList = new SelectList(settingsDBContext.CustomerInfoModels.ToList(), "CustomerName", "CustomerName");
            return View(students.ToPagedList(pageNumber, pageSize));//db.AccountVoucherModels.ToList());

            //return View(db.ProcurementAndTenderModels.Where(x=> x.TenderNo!= null && x.ApprovalLetterNo == null && x.PurchaseOrderNo == null));
        }
        [Authorize(Roles = "Admin,CR,IT")]
        public ActionResult NotYetSuppliedReport()
        {
            
            return View(db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo == null && x.CRPurchaseOrderNo == null));
        }

        [Authorize(Roles = "Admin,CINS,IT")]
        public ActionResult NotYetInspected()
        {
            return View(db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo == null && x.CINSCRNo == null));
        }
        [Authorize(Roles = "Admin,CINS,IT")]
        public ActionResult InspectedResult()
        {
            return View(db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo != null && x.CINSCRNo != null && x.BillPurchaseOrderNo == null && x.BillCRNo == null));
        }
        [Authorize(Roles = "Admin,BILL,IT")]
        public ActionResult waitingForBill()
        {
            return View(db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo != null && x.CINSCRNo != null && x.BillType != "Complete"));
        }
        [AllowAnonymous]
        public ActionResult ProcurementFrontPage()
        {
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();

            ViewBag.created = false;
            var list1 = new List<string>();
            list1.Add("DNS");
            list1.Add("DNST");
            list1.Add("DTS");
            list1.Add("Other");
            ViewBag.departmentlist = list1;//new SelectList(list1);

            ViewBag.denolist = list1;

            return View(db.ProcurementAndTenderModels.Where(x=> x.TenderNo!= null && x.OpeningDate >= DateTime.Today).ToList());
        }
        [Authorize(Roles = "Admin,IT,CST,LP,CR,CINS,BILL")]
        // GET: ProcurementAndTenderModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(Convert.ToInt32(id));//db.ProcurementAndTenderModels.FirstOrDefault(m => m.ID == id); ;
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT")]
        // GET: ProcurementAndTenderModels/Create
        public ActionResult Create()
        {
            ViewBag.error = "";
            allViewbags();
            return View();
        }

        private void allViewbags()
        {
            var departmentlist = new List<string>();
            departmentlist.Add("DNS");
            departmentlist.Add("DNST");
            departmentlist.Add("DTS");
            departmentlist.Add("Other");
            var denolist = new List<string>();
            denolist.Add("NO"); denolist.Add("SET"); denolist.Add("L/I"); denolist.Add("LTR");
            denolist.Add("MTR"); denolist.Add("LINE"); denolist.Add("CFT"); denolist.Add("PCS"); denolist.Add("PRS"); denolist.Add("SHT");
            denolist.Add("KG"); denolist.Add("BOX"); denolist.Add("PKT"); denolist.Add("COPY"); denolist.Add("RM"); denolist.Add("Roll");
            denolist.Add("Lbs"); denolist.Add("Bottle"); denolist.Add("Feet"); denolist.Add("Metric Ton"); denolist.Add("Other");

            var RemarksList = new List<string>();
            RemarksList.Add("Tender");
            RemarksList.Add("Re-Tender-1");
            RemarksList.Add("Re-Tender-2");
            RemarksList.Add("Re-Tender-3");
            RemarksList.Add("Re-Tender-4");
            RemarksList.Add("Re-Tender-5");

            var CSTActionlist = new List<string>();
            CSTActionlist.Add("Tender Nil");
            CSTActionlist.Add("Order Cancel");

            var TypeOfTenderlist = new List<string>();
            TypeOfTenderlist.Add("Open Tender");
            TypeOfTenderlist.Add("Spot Tender");
            TypeOfTenderlist.Add("Normal Tender");
            TypeOfTenderlist.Add("Short Tender");

            var PurchaseTypelist = new List<string>();
            PurchaseTypelist.Add("TS");
            PurchaseTypelist.Add("NS");
            PurchaseTypelist.Add("MT Spare");
            PurchaseTypelist.Add("Other");

            var PlaceOfDeliverylist = new List<string>();
            PlaceOfDeliverylist.Add("NSD Khulna");
            PlaceOfDeliverylist.Add("NSD Chittagong");
            PlaceOfDeliverylist.Add("NSSD Dhaka");

            var PurchaseOrderNolist = new List<string>();
            var CINSDisposallist = new List<string>();
            CINSDisposallist.Add("Pending");
            CINSDisposallist.Add("Send For User");
            CINSDisposallist.Add("Accepted");
            CINSDisposallist.Add("Not Accepted");

            var BillForwardingPlacelist = new List<string>();
            BillForwardingPlacelist.Add("DTS");
            BillForwardingPlacelist.Add("DNS");
            BillForwardingPlacelist.Add("DNST");
            BillForwardingPlacelist.Add("BUDGET");

            var BillTypelist = new List<string>();
            BillTypelist.Add("Complete");
            BillTypelist.Add("Phase 1");
            BillTypelist.Add("Phase 2");
            BillTypelist.Add("Phase 3");

            ViewBag.departmentlist = new SelectList(departmentlist);
            ViewBag.denolist = new SelectList(denolist);
            ViewBag.Remarkslist = new SelectList(RemarksList);
            ViewBag.CSTActionlist = new SelectList(CSTActionlist);
            ViewBag.TypeOfTenderlist = new SelectList(TypeOfTenderlist);
            ViewBag.PurchaseTypelist = new SelectList(PurchaseTypelist);
            ViewBag.PlaceOfDeliverylist = new SelectList(PlaceOfDeliverylist);


            ViewBag.PlaceOfDeliverylist = new SelectList(PlaceOfDeliverylist);
            ViewBag.PurchaseOrderNolist = new SelectList(db.ProcurementAndTenderModels.Where(x => x.PurchaseOrderNo != null).ToList(), "PurchaseOrderNo", "PurchaseOrderNo");
            ViewBag.CINSDisposallist = new SelectList(CINSDisposallist);

            ViewBag.BillForwardingPlacelist = new SelectList(BillForwardingPlacelist);
            ViewBag.BillTypelist = new SelectList(BillTypelist);
        }
        // POST: ProcurementAndTenderModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ProcurementAndTenderModels procurementAndTenderModels,
            HttpPostedFileBase fileITReference)
        {
            ViewBag.error = "";
            allViewbags();

            if (ModelState.IsValid)
            {
                try
                {
                    db.ProcurementAndTenderModels.Add(procurementAndTenderModels);
                    db.SaveChanges();

                }
                catch (SqlException e)
                {
                    if (e.ErrorCode == 2601)
                    {
                        Response.Write("Student already registered!");
                        return View(procurementAndTenderModels);
                    }

                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.InnerException.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2627)
                    {
                        //Response.Write("LeterNo already registered!");
                        ViewBag.error = "Record already registered!";
                        return View(procurementAndTenderModels);
                    }
                }



                string subPath = "~/ProcurementAndTender/" + "ITRef" + "/" + procurementAndTenderModels.ID.ToString();
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));



                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                check_file(fileITReference, procurementAndTenderModels, "ITRef", procurementAndTenderModels.ID);

                db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(procurementAndTenderModels);
        }
        public void check_file(HttpPostedFileBase file, ProcurementAndTenderModels procurementAndTenderModels,string type, int idNo)
        {
            if (file != null)
            {
                var allowedExtensions = new[] { ".pdf"};
                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + ext; //appending the name with id  
                    // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("~/ProcurementAndTender/" + type + "/" + idNo.ToString()), myfile);
                    if (type.Contains("spec"))
                    {
                        procurementAndTenderModels.SpecURL = Path.Combine("~/ProcurementAndTender/" + type + "/" + idNo.ToString(), myfile);
                    }
                    else if (type.Contains("notice"))
                    {
                        procurementAndTenderModels.NoticeURL = Path.Combine("~/ProcurementAndTender/" + type + "/" + idNo.ToString(), myfile);
                    }
                    else if (type.Contains("ITRef"))
                    {
                        procurementAndTenderModels.ITReferenceURL = Path.Combine("~/ProcurementAndTender/" + type + "/" + idNo.ToString(), myfile);
                    }
                    else if (type.Contains("LPRef"))
                    {
                        procurementAndTenderModels.LPReferenceURL = Path.Combine("~/ProcurementAndTender/" + type + "/" + idNo.ToString(), myfile);
                    }

                    file.SaveAs(path);
                }
            }
        }
        [Authorize(Roles = "Admin,IT")]
        // GET: ProcurementAndTenderModels/Edit/5
        public ActionResult Edit(int id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Where(x=> x.ID == id).AsNoTracking().FirstOrDefault();//FirstOrDefault(m => m.LetterNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT,CST")]
        public ActionResult EditCST(int id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Where(x => x.ID == id).FirstOrDefault();//FirstOrDefault(m => m.LetterNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT,LP")]
        public ActionResult EditLP(int? id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(id); ;//db.ProcurementAndTenderModels.FirstOrDefault(m => m.LetterNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT,CR")]
        public ActionResult EditCR(int? id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(id);//db.ProcurementAndTenderModels.FirstOrDefault(m => m.LetterNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT,CINS")]
        public ActionResult EditCINS(int? id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(id);//db.ProcurementAndTenderModels.FirstOrDefault(m => m.LetterNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        [Authorize(Roles = "Admin,IT,BILL")]
        public ActionResult EditBill(int? id)
        {
            allViewbags();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Where(m=> m.ID == id).FirstOrDefault();//.FirstOrDefault(m => m == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }
        // POST: ProcurementAndTenderModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProcurementAndTenderModels procurementAndTenderModels,
            //,TenderNo,CSTDate,OpeningDate,Remarks,SpecURL,NoticeURL,NumberOfPages,price,CSTForwardedTo,CSTAction,TenderNew,TypeOfTender, ApprovalLetterNo,LPDate,PurchaseType,DescriptionOfItems,PurchaseOrderNo,OrderDate,LPQuantity,LPAmount,lpDeno,Days,LastDateOfSupply,NameOfTheSupplier,PlaceOfDelivery,TimeExtentionUpto,LPReferenceURL, CRPurchaseOrderNo,CRNo,CRDate,D44BNo,D44BRecievingDate,RecievingQuantity,CRDeno,RequisitionNo,group, CINSPurchaseOrderNo,CINSCRNo,CINSDateOfRecieving,CINSRecievingQuantity,CINSDeno,CINSDisposal,CINSRemarks, BillPurchaseOrderNo,BillCRNo,FinancialCode, BillRecievingDate,BillForwardingPlace,BillForwardingDate,BillRemarks,BillTotalAmount,BFFC,BillType")] ProcurementAndTenderModels procurementAndTenderModels,
            HttpPostedFileBase fileITReference)
        {
            allViewbags();
           

            var PrevpathfileITReference = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.ITReferenceURL).FirstOrDefault()));
            procurementAndTenderModels.ITReferenceURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.ITReferenceURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileITReference) && fileITReference != null)
            {
                System.IO.File.Delete(PrevpathfileITReference);
            }


            string subPath = "~/ProcurementAndTender/" + "ITRef" + "/" + procurementAndTenderModels.ID.ToString();
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));


            if (ModelState.IsValid)
            {

                check_file(fileITReference, procurementAndTenderModels, "ITRef", procurementAndTenderModels.ID);
                try
                {

                    //db.ProcurementAndTenderModels.Attach(procurementAndTenderModels);
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(procurementAndTenderModels);
                }
            }
            return View(procurementAndTenderModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCST(ProcurementAndTenderModels procurementAndTenderModels, HttpPostedFileBase fileTenderNotice, HttpPostedFileBase fileTenderSpecification)
        {
            allViewbags();

            var PrevpathfileTenderNotice = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.NoticeURL).FirstOrDefault()));
            procurementAndTenderModels.NoticeURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.NoticeURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileTenderNotice) && fileTenderNotice != null)
            {
                System.IO.File.Delete(PrevpathfileTenderNotice);
            }

            var PrevpathfileTenderSpecification = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.SpecURL).FirstOrDefault()));
            procurementAndTenderModels.SpecURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.SpecURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileTenderSpecification) && fileTenderSpecification != null)
            {
                System.IO.File.Delete(PrevpathfileTenderSpecification);
            }


            string subPath = "~/ProcurementAndTender/" + "spec" + "/" + procurementAndTenderModels.ID.ToString(); // your code goes here
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            subPath = "~/ProcurementAndTender/" + "notice" + "/" + procurementAndTenderModels.ID.ToString();
            exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));


            if (ModelState.IsValid)
            {

                check_file(fileTenderNotice, procurementAndTenderModels, "notice", procurementAndTenderModels.ID);
                check_file(fileTenderSpecification, procurementAndTenderModels, "spec", procurementAndTenderModels.ID);
                try
                {
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(procurementAndTenderModels);
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }
            return View(procurementAndTenderModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLP(ProcurementAndTenderModels procurementAndTenderModels,
            HttpPostedFileBase fileLPReference)
        {
            allViewbags();

            var PrevpathfileLPReference = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.LPReferenceURL).FirstOrDefault()));
            procurementAndTenderModels.LPReferenceURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.LPReferenceURL).FirstOrDefault();
            if (System.IO.File.Exists(PrevpathfileLPReference) && fileLPReference != null)
            {
                System.IO.File.Delete(PrevpathfileLPReference);
            }


            var subPath = "~/ProcurementAndTender/" + "LPRef" + "/" + procurementAndTenderModels.ID.ToString();
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            if (ModelState.IsValid)
            {

                check_file(fileLPReference, procurementAndTenderModels, "LPRef", procurementAndTenderModels.ID);
                try
                {
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Model is not valid");

                }

            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }
            return View(procurementAndTenderModels);
        }
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCR( ProcurementAndTenderModels procurementAndTenderModels)
        {
            allViewbags();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Model is not valid");
                    //return RedirectToAction("Index");
                }
            }
            return View(procurementAndTenderModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCINS( ProcurementAndTenderModels procurementAndTenderModels)
        {
            allViewbags();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Model is not valid");
                    //return RedirectToAction("Index");
                }
                //return RedirectToAction("Index");
            }
            return View(procurementAndTenderModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBill( ProcurementAndTenderModels procurementAndTenderModels)
        {
            allViewbags();

            if (ModelState.IsValid)
            {
                try
                {
                    db.Set<ProcurementAndTenderModels>().AddOrUpdate(procurementAndTenderModels);
                    //db.Entry(procurementAndTenderModels).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Model is not valid");
                    //return RedirectToAction("Index");
                }
            }
            return View(procurementAndTenderModels);
        }
        //
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ProcurementAndTenderModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(id);//db.ProcurementAndTenderModels.FirstOrDefault(m => m.TenderNo == id);
            if (procurementAndTenderModels == null)
            {
                return HttpNotFound();
            }
            return View(procurementAndTenderModels);
        }

        // POST: ProcurementAndTenderModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            ProcurementAndTenderModels procurementAndTenderModels = db.ProcurementAndTenderModels.Find(id);//db.ProcurementAndTenderModels.FirstOrDefault(m=> m.TenderNo == id);

            var Prevpathfile1 = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.SpecURL).FirstOrDefault()));
            procurementAndTenderModels.SpecURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.SpecURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile1))
            {
                System.IO.File.Delete(Prevpathfile1);
            }

            var Prevpathfile2 = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.NoticeURL).FirstOrDefault()));
            procurementAndTenderModels.SpecURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.NoticeURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile2))
            {
                System.IO.File.Delete(Prevpathfile2);
            }

            var Prevpathfile3 = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.ITReferenceURL).FirstOrDefault()));
            procurementAndTenderModels.ITReferenceURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.ITReferenceURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile3))
            {
                System.IO.File.Delete(Prevpathfile3);
            }

            var Prevpathfile4 = Path.Combine(Server.MapPath(db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.LPReferenceURL).FirstOrDefault()));
            procurementAndTenderModels.LPReferenceURL = db.ProcurementAndTenderModels.Where(m => m.ID == procurementAndTenderModels.ID).Select(m => m.LPReferenceURL).FirstOrDefault();
            if (System.IO.File.Exists(Prevpathfile4))
            {
                System.IO.File.Delete(Prevpathfile4);
            }

            db.ProcurementAndTenderModels.Remove(procurementAndTenderModels);
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
