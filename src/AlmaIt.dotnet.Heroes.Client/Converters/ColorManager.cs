namespace AlmaIt.Dotnet.Heroes.Client.Converters
{
    using System.Drawing;

    /// <summary>
    /// Class of colors management.
    /// </summary>
    public static class ColorManager
    {
        /// <summary>
        /// Extension method that convert <see cref="Color"/> to html code.
        /// </summary>
        /// <param name="color">Color to convert.</param>
        /// <returns>Return the html color code corresponding to Color.</returns>
        public static string ToHtml(this Color color)
        {
            var colorString = string.Empty;

            if (color.IsEmpty)
            {
                return colorString;
            }

            if (color.IsNamedColor)
            {
                // special case due to mismatch between Html and enum spelling
                colorString = color == Color.LightGray ? "LightGrey" : color.Name;
            }
            else
            {
                colorString = $"#{color.R.ToString("X2", null)}{color.G.ToString("X2", null)}{color.B.ToString("X2", null)}";
            }

            return colorString;
        }

        /// <summary>
        /// Extension method to convert <see cref="int"/> rgb color to html code color.
        /// </summary>
        /// <param name="argb">RGB representation of color.</param>
        /// <returns>Return the html color code corresponding to Color.</returns>
        public static string ToHtml(this int argb)
            => Color.FromArgb(argb).ToHtml();
    }
}
