namespace demoCuoiky_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accout",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(maxLength: 50, unicode: false),
                        password = c.String(maxLength: 100),
                        role = c.Byte(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        deptid = c.Int(nullable: false, identity: true),
                        deptname = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.deptid);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        eid = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 30),
                        age = c.Int(),
                        addr = c.String(maxLength: 30),
                        salary = c.Int(),
                        image = c.String(maxLength: 50),
                        deptid = c.Int(),
                    })
                .PrimaryKey(t => t.eid)
                .ForeignKey("dbo.Department", t => t.deptid)
                .Index(t => t.deptid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "deptid", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "deptid" });
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.Accout");
        }
    }
}
