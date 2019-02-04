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
                        ClassAttendanceId = c.String(nullable: false, maxLength: 128),
                        EnrolledDate = c.DateTime(nullable: false),
                        EnrolledBy = c.String(maxLength: 50),
                        NoShow = c.Boolean(nullable: false),
                        Attendee_Id = c.String(maxLength: 128),
                        ScheduledClass_ScheduledClassId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClassAttendanceId)
                .ForeignKey("dbo.GymUsers", t => t.Attendee_Id)
                .ForeignKey("dbo.ScheduledClasses", t => t.ScheduledClass_ScheduledClassId)
                .Index(t => t.Attendee_Id)
                .Index(t => t.ScheduledClass_ScheduledClassId);
            
            CreateTable(
                "dbo.GymUsers",
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
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        GymUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GymUsers", t => t.GymUser_Id)
                .Index(t => t.GymUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        GymUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.GymUsers", t => t.GymUser_Id)
                .Index(t => t.GymUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        GymUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.GymUsers", t => t.GymUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.GymUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.ScheduledClasses",
                c => new
                    {
                        ScheduledClassId = c.String(nullable: false, maxLength: 128),
                        ClassStartTime = c.DateTime(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        ClassType_ClassTypeId = c.String(maxLength: 128),
                        Instructor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ScheduledClassId)
                .ForeignKey("dbo.ClassTypes", t => t.ClassType_ClassTypeId)
                .ForeignKey("dbo.GymUsers", t => t.Instructor_Id)
                .Index(t => t.ClassType_ClassTypeId)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.ClassTypes",
                c => new
                    {
                        ClassTypeId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        ClassColour = c.Int(nullable: false),
                        Difficulty = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ClassTypeId);
            
            CreateTable(
                "dbo.ClassTimetables",
                c => new
                    {
                        ClassTimetableId = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Weekday = c.Int(nullable: false),
                        ClassType_ClassTypeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClassTimetableId)
                .ForeignKey("dbo.ClassTypes", t => t.ClassType_ClassTypeId)
                .Index(t => t.ClassType_ClassTypeId);
            
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
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.ScheduledClasses", "Instructor_Id", "dbo.GymUsers");
            DropForeignKey("dbo.ScheduledClasses", "ClassType_ClassTypeId", "dbo.ClassTypes");
            DropForeignKey("dbo.ClassTimetables", "ClassType_ClassTypeId", "dbo.ClassTypes");
            DropForeignKey("dbo.ClassAttendances", "ScheduledClass_ScheduledClassId", "dbo.ScheduledClasses");
            DropForeignKey("dbo.IdentityUserRoles", "GymUser_Id", "dbo.GymUsers");
            DropForeignKey("dbo.IdentityUserLogins", "GymUser_Id", "dbo.GymUsers");
            DropForeignKey("dbo.ClassAttendances", "Attendee_Id", "dbo.GymUsers");
            DropForeignKey("dbo.IdentityUserClaims", "GymUser_Id", "dbo.GymUsers");
            DropIndex("dbo.ClassTimetables", new[] { "ClassType_ClassTypeId" });
            DropIndex("dbo.ScheduledClasses", new[] { "Instructor_Id" });
            DropIndex("dbo.ScheduledClasses", new[] { "ClassType_ClassTypeId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "GymUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "GymUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "GymUser_Id" });
            DropIndex("dbo.ClassAttendances", new[] { "ScheduledClass_ScheduledClassId" });
            DropIndex("dbo.ClassAttendances", new[] { "Attendee_Id" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Holidays");
            DropTable("dbo.ClassTimetables");
            DropTable("dbo.ClassTypes");
            DropTable("dbo.ScheduledClasses");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.GymUsers");
            DropTable("dbo.ClassAttendances");
        }
    }
}
