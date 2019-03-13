using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.ClassTimetable
{
    public class ClassTimetableRowViewModel
    {
        public ClassTimetableRowViewModel()
        {
        }

        public ClassTimetableRowViewModel(int hour)
        {
            StartHour = hour;
        }

        public int StartHour { get; set; }
        public ClassTimetableSlotViewModel MondayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel TuesdayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel WednesdayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel ThursdayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel FridayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel SaturdayTimeSlot { get; set; }
        public ClassTimetableSlotViewModel SundayTimeSlot { get; set; }
    }
}