using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Queries;
using System.Data.Entity;

namespace ProperArch01.Persistence.Queries
{
    public class GymUserReader : IGymUserReader
    {
        private readonly ProperArch01DbContext _context;

        public GymUserReader(ProperArch01DbContext context)
        {
            _context = context;
        }
        public async Task<GymUserDto> GetUser(string id)
        {
            var gymUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var roleId = gymUser.Roles.FirstOrDefault().RoleId;

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            var roleName = role.Name;

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

        public async Task<IList<GymUserDto>> GetAllUsers()
        {
            var roles = await _context.Roles.ToListAsync();
            

            var gymUsers = await _context.Users.ToListAsync();

            var dtos = gymUsers.Select(x => new GymUserDto() {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                RoleName = roles.FirstOrDefault(r => r.Id == x.Roles.FirstOrDefault().RoleId).Name
            }).ToList();

            return dtos;
        }

        public async Task<string> GetRoleNameByUser(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userRole = user.Roles.FirstOrDefault();

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
            var roleName = role.Name;

            return roleName;
        }
    }
}