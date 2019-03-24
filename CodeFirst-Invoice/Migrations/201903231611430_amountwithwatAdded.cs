namespace CodeFirst_Invoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amountwithwatAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceDetails", "AmountWithVAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceDetails", "AmountWithVAT");
        }
    }
}
