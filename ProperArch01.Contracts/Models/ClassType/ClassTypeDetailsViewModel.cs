using ProperArch01.Contracts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.ClassType
{
    public class ClassTypeDetailsViewModel
    {
        public string Name { get; set; }
        public Colours.Colour ClassColour { get; set; }
        public int Difficulty { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }

        public List<UpcomingClassesViewModel> UpcomingScheduledClasses { get; set; }
    }
}