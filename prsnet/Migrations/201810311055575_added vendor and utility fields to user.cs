namespace prsnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvendorandutilityfieldstouser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10),
                        Name = c.String(maxLength: 30),
                        Address = c.String(maxLength: 30),
                        City = c.String(maxLength: 30),
                        State = c.String(maxLength: 2),
                        Zip = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 12),
                        Email = c.String(maxLength: 255),
                        IsPreapproved = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        UpdatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true, name: "IDX_Vendor_Code");
            
            AddColumn("dbo.Users", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Users", "UpdatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vendors", "IDX_Vendor_Code");
            DropColumn("dbo.Users", "UpdatedBy");
            DropColumn("dbo.Users", "DateUpdated");
            DropColumn("dbo.Users", "DateCreated");
            DropColumn("dbo.Users", "Active");
            DropTable("dbo.Vendors");
        }
    }
}
