using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class AdminDBContext : DbContext
    {
        static AdminDBContext()
        {
            //Database.SetInitializer<AdminDBContext>(new DropCreateDatabaseIfModelChanges<AdminDBContext>());
            //Database.SetInitializer<AccGardeniaDBContext>(new DropCreateDatabaseAlways<AccGardeniaDBContext>());
        }
        public AdminDBContext() : base("AdmindbBSDKhulna")
        {
        }

        public System.Data.Entity.DbSet<BSDKhulna.Models.ProcurementAndTenderModels> ProcurementAndTenderModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.GeneralNoticeModels> GeneralNoticeModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.CashPPCModels> CashPPCModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.LDModels> LDModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.fonts.CompanyNameModels> CompanyNameModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.SupplierGroupModels> SupplierGroupModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.SupplierModels> SupplierModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.welcomeNoticeModels> welcomeNoticeModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.ContactModels> ContactModels { get; set; }

        public System.Data.Entity.DbSet<BSDKhulna.Models.SuplierUploadModels> SuplierUploadModels { get; set; }
    }


}