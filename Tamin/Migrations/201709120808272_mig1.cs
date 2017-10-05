namespace Tamin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CommentCounter", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "PageCounter", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "Comment_conut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Comment_conut", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "PageCounter");
            DropColumn("dbo.Posts", "CommentCounter");
        }
    }
}
