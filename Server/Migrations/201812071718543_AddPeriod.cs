namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPeriod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PayPeriod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PayPeriod");
        }
    }
}

