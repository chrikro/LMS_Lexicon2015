namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseOccasions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseOccasions", "GroupId", "dbo.Groups");
            DropIndex("dbo.CourseOccasions", new[] { "GroupId" });
            DropTable("dbo.CourseOccasions");
        }
    }
}
