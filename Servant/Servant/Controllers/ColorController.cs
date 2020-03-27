using Servant.Models;

namespace Servant.Controllers
{
    public static class ColorController
    {
        /// <summary>
        /// Method to get selected background color
        /// </summary>
        public static string GetColor()
        {
            return ColorModel.GetColor();
        }

        /// <summary>
        /// Method to save or update a Color
        /// </summary>
        public static void SaveColor(string newColor)
        {
            ColorModel.SaveColor(newColor);
        }
    }
}
