using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProperArch01.Persistence.EntityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ProperArch01.Persistence.EntityModels
{
    public class GymUser : IdentityUser, IEntity
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<GymUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
        public virtual ICollection<ClassAttendance> ClassAttendances { get; set; }
        public virtual ICollection<ScheduledClass> ScheduledClasses { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}