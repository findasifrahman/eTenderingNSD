namespace BSDKhulna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminDBKhulna1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuplierUploadModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LetterID = c.Int(nullable: false),
                        SupplierID = c.String(nullable: false),
                        SupplierContact = c.String(nullable: false),
                        Quantity = c.String(nullable: false),
                        Deno = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        QuoteDocURL = c.String(),
                        Other1DocURL = c.String(),
                        Other2DocURL = c.String(),
                        Other3DocURL = c.String(),
                        Other4DocURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SuplierUploadModels");
        }
    }
}
