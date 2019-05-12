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
        IEnumerable<string> AddGymUser(GymUserDto model);
        bool EditUser(GymUserDto gymUser);
        bool DeleteUser(GymUserDto gymUser);
    }
}
