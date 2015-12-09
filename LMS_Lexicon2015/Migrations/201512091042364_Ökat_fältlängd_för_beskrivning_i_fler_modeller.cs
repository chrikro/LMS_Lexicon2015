namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ökat_fältlängd_för_beskrivning_i_fler_modeller : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Groups", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
