namespace ProperArch01.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToClassType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassTypes", "ImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassTypes", "ImageFileName");
        }
    }
}
