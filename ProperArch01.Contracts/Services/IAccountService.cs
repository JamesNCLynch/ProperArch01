using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Models.Manage;

namespace ProperArch01.Contracts.Services
{
    public interface IAccountService
    {
        Task<GymUserDto> GetUser(string id);
        Task<IList<GymUserDto>> GetAllUsers();
        Task<GymUserDto> GetUserByEmailAddress(string email);
        Task<IEnumerable<string>> AddUserByRegistration(GymUserDto model);
        Task<IEnumerable<string>> AddUserByPortal(CreateUserViewModel model);
        Task<bool> EditUser(GymUserDto gymUser);
        Task<bool> DeleteUser(GymUserDto gymUser);
        Task<string> GetRoleNameByUser(string id);
        Task<AccountIndexViewModel> BuildAccountIndexViewModel(string userId);
    }
}
