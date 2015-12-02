namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActivityTypes");
        }
    }
}
