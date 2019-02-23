using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProperArch01.Persistence.EntityModels;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Persistence.Commands
{
    public class GymUserWriter : IGymUserWriter
    {
        private readonly ProperArch01DbContext _context;

        public GymUserWriter(ProperArch01DbContext context)
        {
            _context = context;
        }
        public IEnumerable<string> AddGymUser(GymUserDto model)
        {
            var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

            // replace with automapper at some point
            var gymUser = new GymUser()
            {
                Email = model.Email,
                UserName = model.UserName?? model.Email,
                FirstName = model.FirstName?? "",
                LastName = model.LastName?? "",
                DateCreated = DateTime.UtcNow
            };

            var result = userManager.Create(gymUser, model.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(gymUser.Id, RoleNames.AttendeeName);
                return null;
            }

            return result.Errors;
        }

        //public IEnumerable<string> AddGymUser(GymUserDto model)
        //{
        //    var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

        //    var gymUser = new EntityModels.GymUser() {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        DateCreated = DateTime.UtcNow                
        //    };

        //    var result = userManager.Create(gymUser, model.Password);
        //    if (result.Succeeded)
        //    {
        //        userManager.AddToRole(gymUser.Id, RoleNames.AttendeeName);
        //        return null;
        //    }

        //    return result.Errors;
        //}

        public IEnumerable<string> AddGymUser(CreateUserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}