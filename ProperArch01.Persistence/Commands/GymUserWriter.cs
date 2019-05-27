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
using NLog;

namespace ProperArch01.Persistence.Commands
{
    public class GymUserWriter : IGymUserWriter
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public GymUserWriter(ProperArch01DbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> AddGymUser(GymUserDto dto)
        {
            try
            {
                var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

                var gymUser = new GymUser()
                {
                    Email = dto.Email,
                    UserName = dto.UserName ?? dto.Email,
                    FirstName = dto.FirstName ?? "",
                    LastName = dto.LastName ?? "",
                    DateCreated = DateTime.UtcNow
                };

                var result = userManager.Create(gymUser, dto.Password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(gymUser.Id, RoleNames.AttendeeName);
                    _logger.Info($"Gym user ID {gymUser.Id} successfully created");
                    return null;
                }
                return result.Errors;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new List<string>() { "Error occurred creating a new member" };
            }
        }

        public bool DeleteUser(GymUserDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var user = _context.Users.FirstOrDefault(x => x.Id == dto.Id);
                if (user == null)
                {
                    _logger.Warn($"User ID {dto.Id} not found in database");
                    return false;
                }

                // if instructor, remove association between user and scheduled class. do no remove scheduled class
                var scheduledClasses = _context.ScheduledClasses.Where(x => x.InstructorId == dto.Id).ToList();
                if (scheduledClasses != null && scheduledClasses.Any())
                {
                    scheduledClasses.ForEach(sc => { sc.InstructorId = null; });
                    _logger.Info($"Instructor ID {dto.Id} removed from scheduled classes with IDs {scheduledClasses.Select(x => x.Id).ToList()}");
                }
                

                // remove associated attendances
                var attendances = _context.ClassAttendances.Where(x => x.AttendeeId == dto.Id).ToList();
                if (attendances != null && attendances.Any())
                {
                    _context.ClassAttendances.RemoveRange(attendances);
                    _logger.Info($"Gym user ID {dto.Id} unsigned from class attendances with IDs {attendances.Select(x => x.Id).ToList()}");
                }

                //remove userroles
                var userRoles = _context.UserRoles.Where(x => x.UserId == dto.Id).ToList();
                _context.UserRoles.RemoveRange(userRoles);

                _context.Users.Remove(user);
                _context.SaveChanges();

                _logger.Info($"Gym user ID {dto.Id} successfully deleted");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool EditUser(GymUserDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.Warn("Parameter passed is null");
                    return false;
                }

                var userManager = new UserManager<GymUser>(new UserStore<GymUser>(_context));

                //var user = _context.Users.FirstOrDefault(x => x.Id == gymUser.Id);
                var user = userManager.Users.FirstOrDefault(x => x.Id == dto.Id);

                if (user == null)
                {
                    _logger.Warn($"Gymuser ID {dto.Id} not found in database");
                    return false;
                }

                // todo: replace with automapper
                user.DateModified = DateTime.UtcNow;
                user.Email = dto.Email;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.UserName = dto.UserName;

                var currentRoleId = user.Roles.FirstOrDefault().RoleId;
                var currentRoleName = _context.Roles.FirstOrDefault(x => x.Id == currentRoleId).Name;

                if (currentRoleName != dto.RoleName)
                {
                    userManager.RemoveFromRole(user.Id, currentRoleName);
                    userManager.AddToRole(user.Id, dto.RoleName);
                    _logger.Info($"Gymuser ID {dto.Id} has changed role from {currentRoleName} to {dto.RoleName}");
                }


                // todo: might have to do something with password soon

                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                _logger.Info($"Gymuser ID {dto.Id} has been updated");

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }
    }
}