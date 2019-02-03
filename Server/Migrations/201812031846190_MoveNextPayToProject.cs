namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveNextPayToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "NextPay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "NextPay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "NextPay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Projects", "NextPay");
        }
    }
}
