namespace ProperArch01.Persistence.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ProperArch01.Persistence.EntityModels;
    using ProperArch01.Contracts.Constants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ProperArch01.Persistence.ProperArch01DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProperArch01.Persistence.ProperArch01DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SeedRolesAndUsers(context);
            SeedInitialClassTypes(context);
            SeedInitialClassTimetable(context);
        }

        private void SeedInitialClassTimetable(ProperArch01DbContext context)
        {
            context.ClassTimetable.AddOrUpdate(
                new ClassTimetable
                {
                    Id = "B95FD7FE-748C-40B3-A2EE-407AFFB8335D",
                    ClassTypeId = "2A19AFC2-B48D-4A9A-B329-DDC0A2827A39",
                    StartTime = new DateTime(2000, 1, 1, 9, 0, 0),
                    EndTime = new DateTime(2000, 1, 1, 10, 0, 0),
                    Weekday = DayOfWeek.Monday
                }
            );
            context.ClassTimetable.AddOrUpdate(
                new ClassTimetable
                {
                    Id = "D3915514-6D05-4D6B-8DB9-DFB5D06BFB10",
                    ClassTypeId = "2A19AFC2-B48D-4A9A-B329-DDC0A2827A39",
                    StartTime = new DateTime(2000, 1, 1, 9, 0, 0),
                    EndTime = new DateTime(2000, 1, 1, 10, 0, 0),
                    Weekday = DayOfWeek.Wednesday
                }
            );
            context.ClassTimetable.AddOrUpdate(
                new ClassTimetable
                {
                    Id = "DBECDEBE-4B79-4B9E-BE41-F446EB1D5987",
                    ClassTypeId = "2A19AFC2-B48D-4A9A-B329-DDC0A2827A39",
                    StartTime = new DateTime(2000, 1, 1, 9, 0, 0),
                    EndTime = new DateTime(2000, 1, 1, 10, 0, 0),
                    Weekday = DayOfWeek.Friday
                }
            );
            context.ClassTimetable.AddOrUpdate(
                new ClassTimetable
                {
                    Id = "5D1ED194-C83A-42C2-BF5F-8C44B3987D6C",
                    ClassTypeId = "E46A1C73-8E07-47E4-8B47-E16922EB0C8F",
                    StartTime = new DateTime(2000, 1, 1, 11, 0, 0),
                    EndTime = new DateTime(2000, 1, 1, 12, 0, 0),
                    Weekday = DayOfWeek.Tuesday
                }
            );
            context.ClassTimetable.AddOrUpdate(
                new ClassTimetable
                {
                    Id = "44C5A47F-E449-47AC-910A-429249A73B63",
                    ClassTypeId = "E05D837C-0D7C-440E-BE69-F7EB4D5F595A",
                    StartTime = new DateTime(2000, 1, 1, 15, 0, 0),
                    EndTime = new DateTime(2000, 1, 1, 16, 0, 0),
                    Weekday = DayOfWeek.Saturday
                }
            );
            context.SaveChanges();
        }

        private void SeedInitialClassTypes(ProperArch01DbContext context)
        {
            context.ClassTypes.AddOrUpdate(
                new ClassType
                {
                    Id = "2A19AFC2-B48D-4A9A-B329-DDC0A2827A39",
                    Name = "Barbell Blast",
                    Difficulty = 80,
                    IsActive = true,
                    ClassColour = Colours.Colour.GoldenRod,
                    Description = "This class is for anyone looking to get lean, toned and fit – fast. Using light to moderate weights with lots of repetition, barbell blast gives you a total body workout. You’ll leave the class feeling challenged and motivated, ready to come back for more.",
                    ImageFileName = "barbell-power-sm.jpg"
                }
            );
            context.ClassTypes.AddOrUpdate(
                new ClassType
                {
                    Id = "E46A1C73-8E07-47E4-8B47-E16922EB0C8F",
                    Name = "Rebel Training",
                    Difficulty = 90,
                    IsActive = true,
                    ClassColour = Colours.Colour.Red,
                    Description = "A high intensity cardio, strength and conditioning class that will push your body to its upper limits. You will get the ultimate body workout, pushing yourself further and performing at a higher intensity. (This class is recommended for those with a good level of fitness).",
                    ImageFileName = "mlc-rebel-sm.jpg"
                }
            );
            context.ClassTypes.AddOrUpdate(
                new ClassType
                {
                    Id = "E05D837C-0D7C-440E-BE69-F7EB4D5F595A",
                    Name = "Fab Abs",
                    Difficulty = 60,
                    IsActive = true,
                    ClassColour = Colours.Colour.SkyBlue,
                    Description = "This is a 30 minute intense workout for your core where you will learn new exercises to help sculpt the body. This class is for all fitness levels and is a great way to fit a quick but challenging workout into your busy day.",
                    ImageFileName = "mlc-abs.jpg"
                }
            );
            context.SaveChanges();
        }

        private void SeedRolesAndUsers(ProperArch01DbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<GymUser>(new UserStore<GymUser>(context));

            if (!roleManager.RoleExists(RoleNames.AdminName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.AdminName
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var user = new GymUser
                {
                    UserName = "admin@mlc.com",
                    Email = "admin@mlc.com",
                    FirstName = "Administrator",
                    LastName = "One"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNames.AdminName);

                }
            }

            if (!roleManager.RoleExists(RoleNames.AttendeeName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.AttendeeName
                };
                roleManager.Create(role);

                var user = new GymUser
                {
                    UserName = "attendee@mlc.com",
                    Email = "attendee@mlc.com",
                    FirstName = "Attendee",
                    LastName = "One"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNames.AttendeeName);
                }
            }

            if (!roleManager.RoleExists(RoleNames.InstructorName))
            {
                var role = new IdentityRole
                {
                    Name = RoleNames.InstructorName
                };
                roleManager.Create(role);

                var user = new GymUser
                {
                    UserName = "instructor@mlc.com",
                    Email = "instructor@mlc.com",
                    FirstName = "Instructor",
                    LastName = "One"
                };

                string userPWD = "Ballygowan1!";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, RoleNames.InstructorName);
                }
            }
        }
    }
}
