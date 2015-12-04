namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseOccasions", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseOccasions", "Name", c => c.String());
        }
    }
}
