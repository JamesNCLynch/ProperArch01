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
            throw new NotImplementedException();
        }

        public GymUserDto GetUserByEmail(string emailAddress)
        {
            var gymUser = _context.Users.FirstOrDefault(x => x.Email == emailAddress);

            return new GymUserDto()
            {
                Id = gymUser.Id,
                Email = gymUser.Email,
                FirstName = gymUser.FirstName,
                LastName = gymUser.LastName,
                UserName = gymUser.UserName
            };
        }

        public IList<GymUserDto> GetUsers(List<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}