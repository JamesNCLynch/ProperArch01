using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Dto;

namespace ProperArch01.Contracts.Models.ClassType
{
    public class EditClassTypeViewModel
    {
        public EditClassTypeViewModel()
        {

        }

        public EditClassTypeViewModel(ClassTypeDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            ClassColour = dto.ClassColour;
            Difficulty = dto.Difficulty;
            Description = dto.Description;
            IsActive = dto.IsActive;
            ImageFileName = dto.ImageFileName;
        }

        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public Colours.Colour ClassColour { get; set; }
        public int Difficulty { get; set; }
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ImageFileName { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}