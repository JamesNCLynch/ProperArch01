using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Contracts.Dto
{
    public class ClassTypeDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Colours.Colour ClassColour { get; set; }
        public int Difficulty { get; set; }
        public string Description { get; set; }
    }
}