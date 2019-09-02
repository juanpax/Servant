using System.Collections.Generic;

namespace Servant
{
    public static class BlurbController
    {
        /// <summary>
        /// Method to get an specific blurb based on a pattern 
        /// </summary>
        public static string[] GetBlurb(string pattern)
        {
            return BlurbModel.GetBlurb(pattern);
        }

        /// <summary>
        /// Method to get all the list of blurb in the database
        /// </summary>
        public static List<string[]> GetBlurbList()
        {
            return BlurbModel.GetBlurbList();
        }

        /// <summary>
        /// Method to save or update a blurb
        /// </summary>
        public static bool SaveBlurb(string id, string pattern, string format, string text)
        {
            return BlurbModel.SaveBlurb(id, pattern, format, text);
        }

        /// <summary>
        /// Method to delete an existing blurb based on its Id
        /// </summary>
        public static bool DeleteBlurb(string id)
        {
            return BlurbModel.DeleteBlurb(id);
        }
    }
}
