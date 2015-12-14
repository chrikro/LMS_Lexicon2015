namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nameiactivitymodelharfåttlängdkontroll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "Name", c => c.String());
        }
    }
}
