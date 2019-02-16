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

    internal sealed class Configuration : DbMigrationsConfiguration<ProperArch01.Persistence.ProperArch01DbContext>
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

            SeedRolesAndUsers();
        }

        private void SeedRolesAndUsers()
        {
            var context = new ProperArch01DbContext();

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
                    UserName = "admin",
                    Email = "jamesbernardlynch@gmail.com"
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
                    UserName = "attendee",
                    Email = "corpsemuncher@gmail.com"
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
                    UserName = "instructor",
                    Email = "jlynch@actionpoint.ie"
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
