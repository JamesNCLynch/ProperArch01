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

namespace ProperArch01.Persistence.Commands
{
    public class GymUserWriter : IGymUserWriter
    {
        private readonly ProperArch01DbContext _context;

        public GymUserWriter(ProperArch01DbContext context)
        {
            _context = context;
        }
        public IEnumerable<string> AddGymUser(RegisterViewModel model)
        {
            var userManager = new UserManager<EntityModels.GymUser>(new UserStore<EntityModels.GymUser>(_context));

            // replace with automapper at some point
            var gymUser = new EntityModels.GymUser()
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

        public IEnumerable<string> AddGymUser(CreateUserViewModel model)
        {
            var userManager = new UserManager<EntityModels.GymUser>(new UserStore<EntityModels.GymUser>(_context));

            var gymUser = new EntityModels.GymUser() {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
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
    }
}