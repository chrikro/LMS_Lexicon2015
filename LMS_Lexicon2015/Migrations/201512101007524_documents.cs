namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Description = c.String(maxLength: 3000),
                        Timestamp = c.DateTime(nullable: false),
                        Deadline = c.DateTime(),
                        UserId = c.String(),
                        GroupId = c.Int(),
                        CourseOccasionId = c.Int(),
                        ActivityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documents");
        }
    }
}
