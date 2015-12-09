namespace LMS_Lexicon2015.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ökat_fältlängd_för_beskrivningsfält_till_3000tkn_i_modelerna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 3000));
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String(nullable: false, maxLength: 3000));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.CourseOccasions", "Description", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Activities", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
