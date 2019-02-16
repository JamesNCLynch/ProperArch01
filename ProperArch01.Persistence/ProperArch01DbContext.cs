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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymUser>().ToTable("AspNetUser");
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaim");
            modelBuilder.Entity<IdentityUserRole>().HasKey(iur => new { iur.UserId, iur.RoleId }).ToTable("AspNetUserRole");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityRole>()
                .HasMany(e => e.Users)
                .WithRequired()
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<GymUser>()
                .HasMany(e => e.Roles)
                .WithRequired()
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<IdentityUserClaim>()
            //    .HasOptional(e => e.UserId)
            //    .WithRequired()
            //    .HasForeignKey(e => e)
            //    .WillCascadeOnDelete(true);

            modelBuilder.Entity<GymUser>()
                .HasMany(e => e.Claims)
                .WithRequired()
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}