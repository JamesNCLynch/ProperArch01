using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Models.Home
{
    public class GalleryViewModel
    {
        public FileUploadModel FileUploadModel { get; set; }
        public List<string> GalleryFileList { get; set; }
        //public HttpPostedFileBase[] Files { get; set; }
    }
}