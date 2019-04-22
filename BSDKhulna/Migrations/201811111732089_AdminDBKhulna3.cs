namespace BSDKhulna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminDBKhulna3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierModels", "password", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierModels", "password");
        }
    }
}
