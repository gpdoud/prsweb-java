namespace prsnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlengthsandrequiredtoUserdata : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "Firstname", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "Lastname", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 12));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 255));
            CreateIndex("dbo.Users", "Username", unique: true, name: "IDX_Username");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "IDX_Username");
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
            AlterColumn("dbo.Users", "Lastname", c => c.String());
            AlterColumn("dbo.Users", "Firstname", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
    }
}
