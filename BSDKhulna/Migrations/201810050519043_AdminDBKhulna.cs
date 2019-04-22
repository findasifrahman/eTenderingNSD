namespace BSDKhulna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminDBKhulna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashPPCModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenderNo = c.String(nullable: false),
                        DescriptionOfItem = c.String(nullable: false),
                        PPCDate = c.DateTime(nullable: false),
                        TotalPages = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                        IDCardNo = c.String(),
                        Name = c.String(nullable: false),
                        NameOfTheFirm = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyNameModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        OwnerName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        duty = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressLine3 = c.String(),
                        AddressLine4 = c.String(),
                        cell = c.String(),
                        phone = c.String(),
                        fax = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GeneralNoticeModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DescriptionOfNotice = c.String(nullable: false),
                        NoticeDownloadUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LDModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseOrderNo = c.String(nullable: false),
                        LDReferenceNo = c.String(nullable: false),
                        PODate = c.DateTime(nullable: false),
                        CompanyName = c.String(nullable: false),
                        LDAmount = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                        Percent = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProcurementAndTenderModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LetterNo = c.String(nullable: false),
                        From = c.String(nullable: false),
                        User = c.String(nullable: false),
                        Department = c.String(nullable: false),
                        ItemDescription = c.String(nullable: false),
                        Quantity = c.String(nullable: false),
                        ITDate = c.DateTime(nullable: false),
                        Deno = c.String(nullable: false),
                        ITReferenceURL = c.String(),
                        TenderNo = c.String(),
                        CSTDate = c.DateTime(),
                        OpeningDate = c.DateTime(),
                        Remarks = c.String(),
                        SpecURL = c.String(),
                        NoticeURL = c.String(),
                        NumberOfPages = c.Int(),
                        price = c.Int(),
                        CSTForwardedTo = c.String(),
                        CSTAction = c.String(),
                        TenderNew = c.Boolean(nullable: false),
                        TypeOfTender = c.String(),
                        ApprovalLetterNo = c.String(),
                        LPDate = c.DateTime(),
                        PurchaseType = c.String(),
                        DescriptionOfItems = c.String(),
                        PurchaseOrderNo = c.String(),
                        OrderDate = c.DateTime(),
                        LPQuantity = c.Int(),
                        LPAmount = c.Int(),
                        lpDeno = c.String(),
                        Days = c.Int(),
                        LastDateOfSupply = c.DateTime(),
                        NameOfTheSupplier = c.String(),
                        PlaceOfDelivery = c.String(),
                        TimeExtentionUpto = c.String(),
                        LPReferenceURL = c.String(),
                        CRPurchaseOrderNo = c.String(),
                        CRNo = c.String(),
                        CRDate = c.DateTime(),
                        D44BNo = c.String(),
                        D44BRecievingDate = c.DateTime(),
                        RecievingQuantity = c.String(),
                        CRDeno = c.String(),
                        RequisitionNo = c.String(),
                        group = c.String(),
                        CINSPurchaseOrderNo = c.String(),
                        CINSCRNo = c.String(),
                        CINSDateOfRecieving = c.DateTime(),
                        CINSRecievingQuantity = c.String(),
                        CINSDeno = c.String(),
                        CINSDisposal = c.String(),
                        CINSRemarks = c.String(),
                        BillPurchaseOrderNo = c.String(),
                        BillCRNo = c.String(),
                        FinancialCode = c.String(),
                        BillRecievingDate = c.DateTime(),
                        BillForwardingPlace = c.String(),
                        BillForwardingDate = c.DateTime(),
                        BillRemarks = c.String(),
                        BillTotalAmount = c.String(),
                        BFFC = c.Boolean(nullable: false),
                        BillType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SupplierGroupModels",
                c => new
                    {
                        GroupName = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupName);
            
            CreateTable(
                "dbo.SupplierModels",
                c => new
                    {
                        SupplierName = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false, identity: true),
                        SupplierGroup = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        CellNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        BarCodeNumber = c.String(nullable: false),
                        IDCardNumber = c.String(nullable: false),
                        pictureURL = c.String(),
                        Published = c.Boolean(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.SupplierName);
            
            CreateTable(
                "dbo.welcomeNoticeModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WelcomeNotice = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.welcomeNoticeModels");
            DropTable("dbo.SupplierModels");
            DropTable("dbo.SupplierGroupModels");
            DropTable("dbo.ProcurementAndTenderModels");
            DropTable("dbo.LDModels");
            DropTable("dbo.GeneralNoticeModels");
            DropTable("dbo.ContactModels");
            DropTable("dbo.CompanyNameModels");
            DropTable("dbo.CashPPCModels");
        }
    }
}
