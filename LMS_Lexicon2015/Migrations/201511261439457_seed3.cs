namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "GroupId");
            AddForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "GroupId", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "GroupId" });
        }
    }
}
