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
        public virtual DbSet<ClassType> ScheduledClassTypes { get; set; }
        public virtual DbSet<ClassTimetable> ClassTimetable { get; set; }
        public virtual DbSet<IdentityUserRole> UserRoles { get; set; }
        public virtual DbSet<Holidays> Holiday { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());

            // Define PKs
            //modelBuilder.Entity<ClassAttendance>()
            //    .HasKey(k => k.Id);

            //modelBuilder.Entity<ClassTimetable>()
            //    .HasKey(k => k.Id);

            //modelBuilder.Entity<ClassType>()
            //    .HasKey(k => k.Id);

            //modelBuilder.Entity<Holidays>()
            //    .HasKey(k => k.Id);

            //modelBuilder.Entity<ScheduledClass>()
            //    .HasKey(k => k.Id);

            // Define nullable one-to-many relationships
            //modelBuilder.Entity<ScheduledClass>()
            //    .HasOptional(sc => sc.Instructor)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

            // Define notnull one-to-many relationships
            //modelBuilder.Entity<ClassType>()
            //    .HasRequired(ct => ct.ClassTimetable)
            //    .WithMany()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<ClassType>()
            //    .HasRequired(ct => ct.ScheduledClass)
            //    .WithMany()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<ScheduledClass>()
            //    .HasRequired(sc => sc.ClassAttendances)
            //    .WithMany()
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<GymUser>()
            //    .HasRequired(gu => gu.ClassAttendances)
            //    .WithMany()
            //    .WillCascadeOnDelete(true);
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