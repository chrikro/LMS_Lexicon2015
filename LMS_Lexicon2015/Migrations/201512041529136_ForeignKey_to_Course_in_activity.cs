namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey_to_Course_in_activity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "CourseOccasion_Id", "dbo.CourseOccasions");
            DropIndex("dbo.Activities", new[] { "CourseOccasion_Id" });
            DropColumn("dbo.Activities", "CourseId");
            RenameColumn(table: "dbo.Activities", name: "CourseOccasion_Id", newName: "CourseId");
            AlterColumn("dbo.Activities", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "CourseId");
            AddForeignKey("dbo.Activities", "CourseId", "dbo.CourseOccasions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "CourseId", "dbo.CourseOccasions");
            DropIndex("dbo.Activities", new[] { "CourseId" });
            AlterColumn("dbo.Activities", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Activities", name: "CourseId", newName: "CourseOccasion_Id");
            AddColumn("dbo.Activities", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "CourseOccasion_Id");
            AddForeignKey("dbo.Activities", "CourseOccasion_Id", "dbo.CourseOccasions", "Id");
        }
    }
}
