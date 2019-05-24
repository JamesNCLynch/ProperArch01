using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Contracts.Models.ClassType
{
    public class AddClassTypeViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public Colours.Colour ClassColour { get; set; }
        public int Difficulty { get; set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}