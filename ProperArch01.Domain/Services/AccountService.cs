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
using System.Threading.Tasks;

namespace ProperArch01.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGymUserWriter _gymUserWriter;
        private readonly IGymUserReader _gymUserReader;

        // Need to update Account services and all piping so that Persistence does not map viewmodels directly to entity models

        public AccountService(IGymUserWriter gymUserWriter, IGymUserReader gymUserReader)
        {
            _gymUserWriter = gymUserWriter;
            _gymUserReader = gymUserReader;
        }
        
        public async Task<IList<GymUserDto>> GetInstructorByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<GymUserDto> GetUser(string id)
        {
            var gymUser = await _gymUserReader.GetUser(id);
            return gymUser;
        }

        public async Task<IList<GymUserDto>> GetUsersByScheduledClass(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<GymUserDto>> GetUsers(IList<string> userIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> AddUserByRegistration(GymUserDto dto)
        {
            var listOfErrors = await _gymUserWriter.AddGymUser(dto);

            return listOfErrors;
        }

        public async Task<IEnumerable<string>> AddUserByPortal(CreateUserViewModel viewModel)
        {
            var dto = new GymUserDto()
            {
                Email = viewModel.Email,
                UserName = viewModel.UserName ?? viewModel.Email,
                FirstName = viewModel.FirstName ?? "",
                LastName = viewModel.LastName ?? "",
                Password = viewModel.Password
            };

            var listOfErrors = await _gymUserWriter.AddGymUser(dto);

            return listOfErrors;
        }

        public async Task<GymUserDto> GetUserByEmailAddress(string email)
        {
            var dto = await _gymUserReader.GetAllUsers();
            var user = dto.FirstOrDefault(x => x.Email == email);

            return user;
        }

        public async Task<IList<GymUserDto>> GetAllUsers()
        {
            var users = await _gymUserReader.GetAllUsers();

            return users;
        }

        public async Task<bool> EditUser(GymUserDto gymUser)
        {
            bool result = await _gymUserWriter.EditUser(gymUser);
            return result;
        }

        public async Task<bool> DeleteUser(GymUserDto gymUser)
        {
            bool result = await _gymUserWriter.DeleteUser(gymUser);
            return result;
        }

        public async Task<string> GetRoleNameByUser(string id)
        {
            string roleName = await _gymUserReader.GetRoleNameByUser(id);
            return roleName;
        }
    }
}