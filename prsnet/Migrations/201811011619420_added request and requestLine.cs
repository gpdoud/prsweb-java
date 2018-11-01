namespace prsnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrequestandrequestLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        RequestId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        UpdatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 255),
                        Justification = c.String(maxLength: 255),
                        RejectionReason = c.String(maxLength: 255),
                        DeliveryMode = c.String(maxLength: 30),
                        SubmittedDate = c.DateTime(),
                        Status = c.String(nullable: false, maxLength: 30),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        UpdatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestLines", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Requests", "UserId", "dbo.Users");
            DropForeignKey("dbo.RequestLines", "ProductId", "dbo.Products");
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropIndex("dbo.RequestLines", new[] { "RequestId" });
            DropIndex("dbo.RequestLines", new[] { "ProductId" });
            DropTable("dbo.Requests");
            DropTable("dbo.RequestLines");
        }
    }
}
