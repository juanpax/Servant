using System;
using System.Collections.Generic;

namespace Servant
{
    public static class BlurbController
    {
        public static List<string[]> GetBlurbList()
        {
            return BlurbModel.GetBlurbList();
        }

        public static bool SaveBlurb(string id, string pattern, string text)
        {
            return BlurbModel.SaveBlurb(id, pattern, text);
        }

        public static bool DeleteBlurb(string id)
        {
            return BlurbModel.DeleteBlurb(id);
        }
    }
}
