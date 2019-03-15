using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProperArch01.Contracts.Constants;
using ProperArch01.Contracts.Models.Colour;
using ProperArch01.Contracts.Services;

namespace ProperArch01.Domain.Services
{
    public class ColourServices : IColourServices
    {
        public RGBAModel GetRGBAModelFromColourEnum(Colours.Colour colour)
        {
            var htmlCode = Colours.ColourHtmlCode(colour);

            return GetRGBAModelFromHtmlCode(htmlCode);
        }

        public RGBAModel GetRGBAModelFromHtmlCode(string htmlCode)
        {
            int htmlCodeAsInt = Int32.Parse(htmlCode, System.Globalization.NumberStyles.HexNumber);

            int red = htmlCodeAsInt / 65536;
            int green = (htmlCodeAsInt % 65536) / 256;
            int blue = htmlCodeAsInt % 256;

            var colourModel = new RGBAModel
            {
                Red = red,
                Green = green,
                Blue = blue,
                Opacity = 1.0
            };

            return colourModel;
        }
    }
}