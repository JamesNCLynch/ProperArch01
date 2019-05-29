﻿using System;
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

        public AccountService(IGymUserWriter gymUserWriter, IGymUserReader gymUserReader)
        {
            _gymUserWriter = gymUserWriter;
            _gymUserReader = gymUserReader;
        }
        

        public async Task<GymUserDto> GetUser(string id)
        {
            var gymUser = _gymUserReader.GetUser(id);
            return await Task.FromResult(gymUser);
        }

        public async Task<IEnumerable<string>> AddUserByRegistration(GymUserDto dto)
        {
            var listOfErrors = _gymUserWriter.AddGymUser(dto);

            return await Task.FromResult(listOfErrors);
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

            var listOfErrors = _gymUserWriter.AddGymUser(dto);

            return await Task.FromResult(listOfErrors);
        }

        public async Task<GymUserDto> GetUserByEmailAddress(string email)
        {
            var dto = _gymUserReader.GetAllUsers();
            var user = dto.FirstOrDefault(x => x.Email == email);

            return await Task.FromResult(user);
        }

        public async Task<IList<GymUserDto>> GetAllUsers()
        {
            var users = _gymUserReader.GetAllUsers();

            return await Task.FromResult(users);
        }

        public async Task<bool> EditUser(GymUserDto gymUser)
        {
            bool result = _gymUserWriter.EditUser(gymUser);
            return await Task.FromResult(result);
        }

        public async Task<bool> DeleteUser(GymUserDto gymUser)
        {
            bool result = _gymUserWriter.DeleteUser(gymUser);
            return await Task.FromResult(result);
        }

        public async Task<string> GetRoleNameByUser(string id)
        {
            string roleName = _gymUserReader.GetRoleNameByUser(id);
            return await Task.FromResult(roleName);
        }
    }
}