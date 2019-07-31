using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Constants
{
    public class Colours
    {
        public enum Colour
        {
            Aqua = 0,
            DeepPink = 1,
            DeepSkyBlue = 2,
            GoldenRod = 3,
            MediumOrchid = 4,
            Lime = 5,
            MidnightBlue = 6,
            Navy = 7,
            Plum = 8,
            RebeccaPurple = 9,
            Red = 10,
            Salmon = 11,
            SkyBlue = 12,
            Thistle = 13,
            Violet = 14,
            YellowGreen = 15
        }

        public static string ColourHtmlCode(Colours.Colour colour)
        {
            var retValue = "";
            switch (colour)
            {
                case Colours.Colour.Aqua:
                    retValue = "00FFFF";
                    break;
                case Colours.Colour.DeepPink:
                    retValue = "FF1493";
                    break;
                case Colours.Colour.DeepSkyBlue:
                    retValue = "00BFFF";
                    break;
                case Colour.GoldenRod:
                    retValue = "DAA520";
                    break;
                case Colours.Colour.MediumOrchid:
                    retValue = "BA55D3 ";
                    break;
                case Colours.Colour.Lime:
                    retValue = "00FF00";
                    break;
                case Colours.Colour.MidnightBlue:
                    retValue = "191970";
                    break;
                case Colours.Colour.Navy:
                    retValue = "000080";
                    break;
                case Colours.Colour.Plum:
                    retValue = "DDA0DD";
                    break;
                case Colours.Colour.RebeccaPurple:
                    retValue = "663399";
                    break;
                case Colours.Colour.Red:
                    retValue = "FF0000";
                    break;
                case Colours.Colour.Salmon:
                    retValue = "FA8072";
                    break;
                case Colours.Colour.SkyBlue:
                    retValue = "87CEEB";
                    break;
                case Colours.Colour.Thistle:
                    retValue = "D8BFD8";
                    break;
                case Colours.Colour.Violet:
                    retValue = "EE82EE";
                    break;
                case Colours.Colour.YellowGreen:
                    retValue = "9ACD32";
                    break;
                default:
                    retValue = "FFFFFF";
                    break;
            }

            return retValue;
        }
    }
}