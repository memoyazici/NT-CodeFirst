namespace CodeFirst_Invoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intErrors : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CompanyName", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Address", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "CompanyName", c => c.Int(nullable: false));
        }
    }
}
