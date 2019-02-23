using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Services
{
    public interface IAccountService
    {
        GymUserDto GetUser(string id);
        GymUserDto GetUserByEmailAddress(string email);
        IList<GymUserDto> GetUsers(IList<string> userIds);
        IList<GymUserDto> GetUsersByScheduledClass(string id);
        IList<GymUserDto> GetInstructorByScheduledClass(string id);
        IEnumerable<string> AddUserByRegistration(GymUserDto model);
        IEnumerable<string> AddUserByPortal(CreateUserViewModel model);
    }
}
