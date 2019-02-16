namespace ProperArch01.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassAttendances",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EnrolledDate = c.DateTime(nullable: false),
                        EnrolledBy = c.String(maxLength: 50),
                        AttendeeId = c.String(maxLength: 128),
                        ScheduledClassId = c.String(maxLength: 128),
                        NoShow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUser", t => t.AttendeeId)
                .ForeignKey("dbo.ScheduledClasses", t => t.ScheduledClassId)
                .Index(t => t.AttendeeId)
                .Index(t => t.ScheduledClassId);
            
            CreateTable(
                "dbo.AspNetUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        GymUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUser", t => t.GymUser_Id)
                .Index(t => t.GymUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUser", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ScheduledClasses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClassStartTime = c.DateTime(nullable: false),
                        ClassTypeId = c.String(maxLength: 128),
                        InstructorId = c.String(maxLength: 128),
                        IsCancelled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTypes", t => t.ClassTypeId)
                .ForeignKey("dbo.AspNetUser", t => t.InstructorId)
                .Index(t => t.ClassTypeId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.ClassTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        ClassColour = c.Int(nullable: false),
                        Difficulty = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassTimetables",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClassTypeId = c.String(maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Weekday = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTypes", t => t.ClassTypeId)
                .Index(t => t.ClassTypeId);
            
            CreateTable(
                "dbo.Holidays",
                c => new
                    {
                        HolidaysId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 50),
                        HolidayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HolidaysId);
            
            CreateTable(
                "dbo.AspNetRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRole", "RoleId", "dbo.AspNetRole");
            DropForeignKey("dbo.ScheduledClasses", "InstructorId", "dbo.AspNetUser");
            DropForeignKey("dbo.ScheduledClasses", "ClassTypeId", "dbo.ClassTypes");
            DropForeignKey("dbo.ClassTimetables", "ClassTypeId", "dbo.ClassTypes");
            DropForeignKey("dbo.ClassAttendances", "ScheduledClassId", "dbo.ScheduledClasses");
            DropForeignKey("dbo.AspNetUserRole", "UserId", "dbo.AspNetUser");
            DropForeignKey("dbo.AspNetUserLogins", "GymUser_Id", "dbo.AspNetUser");
            DropForeignKey("dbo.ClassAttendances", "AttendeeId", "dbo.AspNetUser");
            DropForeignKey("dbo.AspNetUserClaim", "UserId", "dbo.AspNetUser");
            DropIndex("dbo.ClassTimetables", new[] { "ClassTypeId" });
            DropIndex("dbo.ScheduledClasses", new[] { "InstructorId" });
            DropIndex("dbo.ScheduledClasses", new[] { "ClassTypeId" });
            DropIndex("dbo.AspNetUserRole", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRole", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "GymUser_Id" });
            DropIndex("dbo.AspNetUserClaim", new[] { "UserId" });
            DropIndex("dbo.ClassAttendances", new[] { "ScheduledClassId" });
            DropIndex("dbo.ClassAttendances", new[] { "AttendeeId" });
            DropTable("dbo.AspNetRole");
            DropTable("dbo.Holidays");
            DropTable("dbo.ClassTimetables");
            DropTable("dbo.ClassTypes");
            DropTable("dbo.ScheduledClasses");
            DropTable("dbo.AspNetUserRole");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaim");
            DropTable("dbo.AspNetUser");
            DropTable("dbo.ClassAttendances");
        }
    }
}
