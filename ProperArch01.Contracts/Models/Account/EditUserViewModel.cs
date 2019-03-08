using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.Account
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {

        }

        public EditUserViewModel(GymUserDto dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            UserName = dto.UserName;
            Email = dto.Email;
            RoleName = dto.RoleName;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Membership number")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}