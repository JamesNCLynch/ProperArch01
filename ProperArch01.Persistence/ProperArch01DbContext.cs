using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Persistence.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using ProperArch01.Persistence.EntityModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProperArch01.Persistence
{
    public class ProperArch01DbContext : IdentityDbContext<GymUser>, IProperArch01DbContext
    {
        public ProperArch01DbContext() : base("name=DefaultConnection")
        {
        }

        public ProperArch01DbContext(string connectionString) : base(connectionString)
        {
        }

        public virtual DbSet<ClassAttendance> ClassAttendances { get; set; }
        public virtual DbSet<ScheduledClass> ScheduledClasses { get; set; }
        public virtual DbSet<ClassType> ClassTypes { get; set; }
        public virtual DbSet<ClassTimetable> ClassTimetable { get; set; }
        public virtual DbSet<IdentityUserRole> UserRoles { get; set; }
        public virtual DbSet<Holidays> Holiday { get; set; }
        public virtual DbSet<GymUser> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }


        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.RoleId);
            }
        }
    }
}