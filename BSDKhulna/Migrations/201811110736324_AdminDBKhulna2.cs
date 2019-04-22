namespace BSDKhulna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminDBKhulna2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SuplierUploadModels");
            AlterColumn("dbo.SuplierUploadModels", "SupplierID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.SuplierUploadModels", new[] { "LetterID", "SupplierID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SuplierUploadModels");
            AlterColumn("dbo.SuplierUploadModels", "SupplierID", c => c.String(nullable: false));
            AddPrimaryKey("dbo.SuplierUploadModels", "ID");
        }
    }
}
