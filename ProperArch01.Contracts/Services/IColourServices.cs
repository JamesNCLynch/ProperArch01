using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProperArch01.Contracts.Models.Colour;
using ProperArch01.Contracts.Constants;

namespace ProperArch01.Contracts.Services
{
    public interface IColourServices
    {
        RGBAModel GetRGBAModelFromHtmlCode(string htmlCode);
        RGBAModel GetRGBAModelFromColourEnum(Colours.Colour colour);
    }
}
