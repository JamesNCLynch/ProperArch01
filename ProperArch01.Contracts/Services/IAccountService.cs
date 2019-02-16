using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Account;

namespace ProperArch01.Contracts.Services
{
    public interface IAccountService
    {
        GymUser GetUser(string id);
        IList<GymUser> GetUsers(IList<string> userIds);
        IList<GymUser> GetUsersByScheduledClass(string id);
        IList<GymUser> GetInstructorByScheduledClass(string id);
        IEnumerable<string> AddUserByRegistration(RegisterViewModel model);
        IEnumerable<string> AddUserByPortal(CreateUserViewModel model);
    }
}
