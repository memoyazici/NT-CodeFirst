namespace CodeFirst_Invoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountwithVATPersisted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InvoiceDetails", "AmountWithVAT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceDetails", "AmountWithVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
