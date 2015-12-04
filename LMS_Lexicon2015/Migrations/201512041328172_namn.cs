namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "CourseOccasion_Id", c => c.Int());
            CreateIndex("dbo.Activities", "CourseOccasion_Id");
            AddForeignKey("dbo.Activities", "CourseOccasion_Id", "dbo.CourseOccasions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "CourseOccasion_Id", "dbo.CourseOccasions");
            DropIndex("dbo.Activities", new[] { "CourseOccasion_Id" });
            DropColumn("dbo.Activities", "CourseOccasion_Id");
        }
    }
}
