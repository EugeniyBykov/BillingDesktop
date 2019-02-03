namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextPayMaybeNULL : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "NextPay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "NextPay", c => c.DateTime(nullable: false));
        }
    }
}
