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
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace ProperArch01.Persistence.Commands
{
    public class GymUserWriter : IGymUserWriter
    {
        private readonly ProperArch01DbContext _context;

        public GymUserWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> AddGymUser(GymUserDto model)
        {
            var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));
            
            var gymUser = new GymUser()
            {
                Email = model.Email,
                UserName = model.UserName?? model.Email,
                FirstName = model.FirstName?? "",
                LastName = model.LastName?? "",
                DateCreated = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(gymUser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(gymUser.Id, RoleNames.AttendeeName);
                return null;
            }

            return result.Errors;
        }

        //public async Task<IEnumerable<string>> AddGymUser(CreateUserViewModel model)
        //{
        //    var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

        //    // replace with automapper at some point
        //    var gymUser = new GymUser()
        //    {
        //        Email = model.Email,
        //        UserName = model.UserName ?? model.Email,
        //        FirstName = model.FirstName ?? "",
        //        LastName = model.LastName ?? "",
        //        DateCreated = DateTime.UtcNow
        //    };

        //    var result = await userManager.CreateAsync(gymUser, model.Password);
        //    if (result.Succeeded)
        //    {
        //        await userManager.AddToRoleAsync(gymUser.Id, RoleNames.AttendeeName);
        //        return null;
        //    }

        //    return result.Errors;
        //}

        public async Task<bool> DeleteUser(GymUserDto gymUser)
        {
            if (gymUser == null)
            {
                return false;
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == gymUser.Id);
            if (user == null)
            {
                return false;
            }

            // if instructor, remove association between user and scheduled class. do no remove scheduled class
            var scheduledClasses = await _context.ScheduledClasses.Where(x => x.InstructorId == gymUser.Id).ToListAsync();
            scheduledClasses.ForEach(sc => { sc.InstructorId = null; });

            // remove associated attendances
            var attendances = await _context.ClassAttendances.Where(x => x.AttendeeId == gymUser.Id).ToListAsync();
            _context.ClassAttendances.RemoveRange(attendances);

            //remove userroles
            var userRoles = await _context.UserRoles.Where(x => x.UserId == gymUser.Id).ToListAsync();
            _context.UserRoles.RemoveRange(userRoles);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditUser(GymUserDto gymUser)
        {
            if (gymUser == null)
            {
                return false;
            }

            var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

            //var user = _context.Users.FirstOrDefault(x => x.Id == gymUser.Id);
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == gymUser.Id);

            if (user == null)
            {
                return false;
            }

            // todo: replace with automapper
            user.DateModified = DateTime.UtcNow;
            user.Email = gymUser.Email;
            user.FirstName = gymUser.FirstName;
            user.LastName = gymUser.LastName;
            user.UserName = gymUser.UserName;

            var currentRoleId = user.Roles.FirstOrDefault().RoleId;
            var currentRoleName = _context.Roles.FirstOrDefault(x => x.Id == currentRoleId).Name;

            if (currentRoleName != gymUser.RoleName)
            {
                await userManager.RemoveFromRoleAsync(user.Id, currentRoleName);
                await userManager.AddToRoleAsync(user.Id, gymUser.RoleName);
            }


            // todo: might have to do something with password soon

            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}