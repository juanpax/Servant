using System;
using System.Data;
using System.IO;

namespace Servant.Models
{
    public static class ColorModel
    {
        // ServantVariables file location
        private static string ServantVariablesFile = Environment.CurrentDirectory + "\\ServantVariables.txt";

        /// <summary>
        /// Method to get selected background color
        /// </summary>
        public static string GetColor()
        {
            string result = "";
            StreamReader file = new StreamReader(ServantVariablesFile);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Color:"))
                {
                    result = line.Replace("Color:", "");
                    break;
                }
            }
            file.Close();
            return result;
        }

        /// <summary>
        /// Method to save or update a Color
        /// </summary>
        public static void SaveColor(string newColor)
        {
            File.WriteAllText(ServantVariablesFile, "Color:" + newColor);
        }
    }
}

