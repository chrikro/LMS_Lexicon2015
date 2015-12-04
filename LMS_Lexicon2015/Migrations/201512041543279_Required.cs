namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Groups", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Description", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String());
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String());
            AlterColumn("dbo.Activities", "Description", c => c.String());
        }
    }
}
