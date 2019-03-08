using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;

namespace ProperArch01.Persistence.Queries
{
    public class GymUserReader : IGymUserReader
    {
        private readonly ProperArch01DbContext _context;

        public GymUserReader(ProperArch01DbContext context)
        {
            _context = context;
        }
        public GymUserDto GetUser(string id)
        {
            var gymUser = _context.Users.FirstOrDefault(x => x.Id == id);

            var roleId = gymUser.Roles.FirstOrDefault().RoleId;

            var roleName = _context.Roles.FirstOrDefault(x => x.Id == roleId).Name;

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
            var roles = _context.Roles.AsEnumerable();

            var gymUsers = _context.Users.Select(x => new GymUserDto() {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                RoleName = roles.FirstOrDefault(r => r.Id == x.Roles.FirstOrDefault().RoleId).Name
            }).ToList();

            return gymUsers;
        }

        public string GetRoleNameByUser(string id)
        {
            //var roleName = _context.Roles
            //    .FirstOrDefault(r => r.Id == _context.UserRoles.FirstOrDefault(x => x.UserId == id).RoleId)
            //    .Name;
            var userRole = _context.Users.FirstOrDefault(x => x.Id == id).Roles.FirstOrDefault();
            var roleName = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId).Name;

            return roleName;
        }
    }
}