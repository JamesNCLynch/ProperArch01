using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProperArch01.Persistence.EntityModels;
using System.Threading.Tasks;

namespace ProperArch01.Persistence.Interfaces
{
    public interface IProperArch01DbContext
    {
        DbSet<ClassAttendance> ClassAttendances { get; set; }
        DbSet<ScheduledClass> ScheduledClasses { get; set; }
        DbSet<ClassType> ScheduledClassTypes { get; set; }
        DbSet<ClassTimetable> ClassTimetable { get; set; }
        DbSet<IdentityUserRole> UserRoles { get; set; }
        DbSet<Holidays> Holiday { get; set; }
        
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}