using System;
using System.Drawing;

namespace AlmaIt.dotnet.Heroes.Client.Converters
{
    public static class ColorManager
    {
        public static string ToHtml(this Color color)
        {
            string colorString = String.Empty;

            if (color.IsEmpty)
            { return colorString; }

            if (color.IsNamedColor)
            {
                if (color == Color.LightGray)
                {
                    // special case due to mismatch between Html and enum spelling
                    colorString = "LightGrey";
                }
                else
                {
                    colorString = color.Name;
                }
            }
            else
            {
                colorString = $"#{color.R.ToString("X2", null)}{color.G.ToString("X2", null)}{color.B.ToString("X2", null)}";
            }

            return colorString;
        }

        public static string ToHtml(this int argb)
        {
            var color = Color.FromArgb(argb);
            return color.ToHtml();
        }
    }
}