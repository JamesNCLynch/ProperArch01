using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ProperArch01.Contracts.Models.Account;
using ProperArch01.Contracts.Models.ClassTimetable;
using ProperArch01.Contracts.Models.ClassType;
using ProperArch01.Persistence.EntityModels;

namespace ProperArch01.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            // Contracts to Entities
            CreateMap<Contracts.Models.Account.GymUser, Persistence.EntityModels.GymUser>();

            // Entities to Contracts
        }
    }
}