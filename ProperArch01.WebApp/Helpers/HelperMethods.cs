using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.WebApp.Models;
using System.Configuration;

namespace ProperArch01.WebApp.Helpers
{
    public static class HelperMethods
    {
        public static List<string> GetAllRoleNames()
        {
            var roles = new List<string>
            {
                RoleNames.AdminName,
                RoleNames.AttendeeName,
                RoleNames.InstructorName
            };

            return roles;
        }

        public static List<int> GetSlotMinutes ()
        {
            return new List<int> {0, 15, 30, 45};
        }

        public static List<int> GetSlotHours()
        {
            var earliestSlotStartHour = Int32.Parse(ConfigurationManager.AppSettings["GymOpeningHour"]);
            var latestSlotEndHour = Int32.Parse(ConfigurationManager.AppSettings["GymClosingHour"]);

            IEnumerable<int> hours = Enumerable.Range(earliestSlotStartHour, latestSlotEndHour - earliestSlotStartHour);

            return hours.ToList();
        }

        // menu lists

        public static List<MenuListItemModel> GetAboutMenu()
        {
            return new List<MenuListItemModel>()
            {
                new MenuListItemModel
                {
                    DisplayName = "Mission/Aim",
                    ControllerName = "Home",
                    ActionName = "MissionAim"
                },
                new MenuListItemModel
                {
                    DisplayName = "History",
                    ControllerName = "Home",
                    ActionName = "History"
                }
            };
        }

        public static List<MenuListItemModel> GetRatesMenu()
        {
            return new List<MenuListItemModel>()
            {
                new MenuListItemModel
                {
                    DisplayName = "Membership Rates",
                    ControllerName = "Home",
                    ActionName = "MembershipRates"
                },
                new MenuListItemModel
                {
                    DisplayName = "Corporate Rates",
                    ControllerName = "Home",
                    ActionName = "CorporateRates"
                },
                new MenuListItemModel
                {
                    DisplayName = "Pay As You Go",
                    ControllerName = "Home",
                    ActionName = "Payg"
                },
                new MenuListItemModel
                {
                    DisplayName = "Room/Facility Rates",
                    ControllerName = "Home",
                    ActionName = "RoomFacilityRates"
                },
                new MenuListItemModel
                {
                    DisplayName = "Booking Forms",
                    ControllerName = "Home",
                    ActionName = "BookingForms"
                }
            };
        }
        public static List<MenuListItemModel> GetFacilitiesMenu()
        {
            return new List<MenuListItemModel>()
            {
                new MenuListItemModel
                {
                    DisplayName = "Sports Hall",
                    ActionName = "SportsHall",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "Gym",
                    ActionName = "Gym",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "Activities Room",
                    ActionName = "Activities",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "All Weather Pitch",
                    ActionName = "Pitch",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "Meeting Rooms",
                    ActionName = "MeetingRooms",
                    ControllerName = "Home"
                },
            };
        }
        public static List<MenuListItemModel> GetActivitiesMenu()
        {
            return new List<MenuListItemModel>()
            {
                new MenuListItemModel
                {
                    DisplayName = "MLC Class and Timetable",
                    ActionName = "Index",
                    ControllerName = "ClassTimetable"
                },
                new MenuListItemModel
                {
                    DisplayName = "Summer Camps",
                    ActionName = "SummerCamps",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "Birthday Parties",
                    ActionName = "Birthday",
                    ControllerName = "Home"
                },
                new MenuListItemModel
                {
                    DisplayName = "Gallery",
                    ActionName = "Gallery",
                    ControllerName = "Home"
                },
            };
        }

        public static List<MenuListItemModel> GetAdminMenu()
        {
            return new List<MenuListItemModel>()
            {
                new MenuListItemModel
                {
                    DisplayName = "Create a class type",
                    ActionName = "Create",
                    ControllerName = "ClassType"
                },
                new MenuListItemModel
                {
                    DisplayName = "Scheduled classes need completion",
                    ActionName = "Index",
                    ControllerName = "ScheduledClass"
                },
                new MenuListItemModel
                {
                    DisplayName = "Add a holiday",
                    ActionName = "Create",
                    ControllerName = "Holiday"
                }
            };
        }
    }
}