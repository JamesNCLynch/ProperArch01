using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Account;

namespace ProperArch01.Contracts.Commands
{
    public interface IGymUserWriter
    {
        IEnumerable<string> AddGymUser(RegisterViewModel model);
        IEnumerable<string> AddGymUser(CreateUserViewModel model);
    }
}
