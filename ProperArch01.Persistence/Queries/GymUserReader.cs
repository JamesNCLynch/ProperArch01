using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;
using NLog;

namespace ProperArch01.Persistence.Queries
{
    public class GymUserReader : IGymUserReader
    {
        private readonly ProperArch01DbContext _context;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public GymUserReader(ProperArch01DbContext context)
        {
            _context = context;
        }
        public GymUserDto GetUser(string id)
        {
            var gymUser = _context.Users.FirstOrDefault(x => x.Id == id);

            if (gymUser == null)
            {
                _logger.Warn($"Gymuser ID {id}");
                return null;
            }

            var roleId = gymUser.Roles.FirstOrDefault().RoleId;

            var role = _context.Roles.FirstOrDefault(x => x.Id == roleId);
            var roleName = role.Name;

            _logger.Info($"Gymuser ID {id} with role {roleName} found in database");

            return new GymUserDto()
            {
                Id = gymUser.Id,
                Email = gymUser.Email,
                FirstName = gymUser.FirstName,
                LastName = gymUser.LastName,
                UserName = gymUser.UserName,
                RoleName = roleName
            };
        }

        public IList<GymUserDto> GetAllUsers()
        {
            var roles = _context.Roles.ToList();

            var gymUsers = _context.Users.ToList();

            var dtos = gymUsers.Select(x => new GymUserDto() {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                RoleName = roles.FirstOrDefault(r => r.Id == x.Roles.FirstOrDefault().RoleId).Name
            }).ToList();

            _logger.Info($"{dtos.Count()} Gymusers found in database");

            return dtos;
        }

        public string GetRoleNameByUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var userRole = user.Roles.FirstOrDefault();

            var role = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);
            var roleName = role.Name;

            _logger.Info($"Role {roleName} found for GymUser {id}");

            return roleName;
        }
    }
}