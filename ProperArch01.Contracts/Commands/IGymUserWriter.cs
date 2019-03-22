using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Commands
{
    public interface IGymUserWriter
    {
        Task<IEnumerable<string>> AddGymUser(GymUserDto model);
        Task<bool> EditUser(GymUserDto gymUser);
        Task<bool> DeleteUser(GymUserDto gymUser);
    }
}
