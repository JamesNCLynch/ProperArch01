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
        Task<GymUserDto> GetUser(string id);
        Task<IList<GymUserDto>> GetAllUsers();
        Task<GymUserDto> GetUserByEmailAddress(string email);
        Task<IList<GymUserDto>> GetUsers(IList<string> userIds);
        Task<IList<GymUserDto>> GetUsersByScheduledClass(string id);
        Task<IList<GymUserDto>> GetInstructorByScheduledClass(string id);
        Task<IEnumerable<string>> AddUserByRegistration(GymUserDto model);
        Task<IEnumerable<string>> AddUserByPortal(CreateUserViewModel model);
        Task<bool> EditUser(GymUserDto gymUser);
        Task<bool> DeleteUser(GymUserDto gymUser);
        Task<string> GetRoleNameByUser(string id);
    }
}
