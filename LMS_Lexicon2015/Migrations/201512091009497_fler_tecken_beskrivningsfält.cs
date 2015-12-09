namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fler_tecken_beskrivningsfält : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
