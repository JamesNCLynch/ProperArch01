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

        public IEnumerable<string> AddGymUser(GymUserDto model)
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

            var result = userManager.Create(gymUser, model.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(gymUser.Id, RoleNames.AttendeeName);
                return null;
            }

            return result.Errors;
        }

        //public IEnumerable<string>> AddGymUser(CreateUserViewModel model)
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

        //    var result = userManager.Create(gymUser, model.Password);
        //    if (result.Succeeded)
        //    {
        //        userManager.AddToRole(gymUser.Id, RoleNames.AttendeeName);
        //        return null;
        //    }

        //    return result.Errors;
        //}

        public bool DeleteUser(GymUserDto gymUser)
        {
            if (gymUser == null)
            {
                return false;
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == gymUser.Id);
            if (user == null)
            {
                return false;
            }

            // if instructor, remove association between user and scheduled class. do no remove scheduled class
            var scheduledClasses = _context.ScheduledClasses.Where(x => x.InstructorId == gymUser.Id).ToList();
            scheduledClasses.ForEach(sc => { sc.InstructorId = null; });

            // remove associated attendances
            var attendances = _context.ClassAttendances.Where(x => x.AttendeeId == gymUser.Id).ToList();
            _context.ClassAttendances.RemoveRange(attendances);

            //remove userroles
            var userRoles = _context.UserRoles.Where(x => x.UserId == gymUser.Id).ToList();
            _context.UserRoles.RemoveRange(userRoles);

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }

        public bool EditUser(GymUserDto gymUser)
        {
            if (gymUser == null)
            {
                return false;
            }

            var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

            //var user = _context.Users.FirstOrDefault(x => x.Id == gymUser.Id);
            var user = userManager.Users.FirstOrDefault(x => x.Id == gymUser.Id);

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
                userManager.RemoveFromRole(user.Id, currentRoleName);
                userManager.AddToRole(user.Id, gymUser.RoleName);
            }


            // todo: might have to do something with password soon

            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

            return true;
        }
    }
}