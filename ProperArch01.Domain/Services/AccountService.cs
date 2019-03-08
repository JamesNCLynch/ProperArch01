using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ProperArch01.Contracts.Commands;
using ProperArch01.Contracts.Queries;
using ProperArch01.Contracts.Dto;
using ProperArch01.Contracts.Constants;


namespace ProperArch01.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGymUserWriter _gymUserWriter;
        private readonly IGymUserReader _gymUserReader;

        //public AccountService()
        //{
        //}

        public AccountService(IGymUserWriter gymUserWriter, IGymUserReader gymUserReader)
        {
            _gymUserWriter = gymUserWriter;
            _gymUserReader = gymUserReader;
        }
        
        public IList<GymUserDto> GetInstructorByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public GymUserDto GetUser(string id)
        {
            var gymUser = _gymUserReader.GetUser(id);
            return gymUser;
        }

        public IList<GymUserDto> GetUsersByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public IList<GymUserDto> GetUsers(IList<string> userIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> AddUserByRegistration(GymUserDto model)
        {
            var listOfErrors = _gymUserWriter.AddGymUser(model);

            return listOfErrors;
        }

        public IEnumerable<string> AddUserByPortal(CreateUserViewModel model)
        {
            var listOfErrors = _gymUserWriter.AddGymUser(model);

            return listOfErrors;
        }

        public GymUserDto GetUserByEmailAddress(string email)
        {
            var user = _gymUserReader.GetAllUsers().FirstOrDefault(x => x.Email == email);

            return user;
        }

        public IList<GymUserDto> GetAllUsers()
        {
            var users = _gymUserReader.GetAllUsers().ToList();

            return users;
        }

        public bool EditUser(GymUserDto gymUser)
        {
            bool result = _gymUserWriter.EditUser(gymUser);
            return result;
        }

        public bool DeleteUser(GymUserDto gymUser)
        {
            bool result = _gymUserWriter.DeleteUser(gymUser);
            return result;
        }

        public string GetRoleNameByUser(string id)
        {
            string roleName = _gymUserReader.GetRoleNameByUser(id);
            return roleName;
        }
    }
}