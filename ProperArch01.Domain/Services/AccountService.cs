using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ProperArch01.Domain.Services
{
    public class AccountService : IAccountService
    {
        
        public AccountService()
        {

        }
        
        public IList<GymUser> GetInstructorByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public GymUser GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public IList<GymUser> GetUsersByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public IList<GymUser> GetUsers(IList<string> userIds)
        {
            throw new NotImplementedException();
        }
    }
}