using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helper
{
    public static class SirriusHelper
    {
        //[Description("Delegate has two parameters, the first is full text, the second is need to be searched text")]
        public static Func<string, string, bool> stringContains = (s, search) => s.Trim().ToLower().Contains(search.Trim().ToLower());

        public static Func<string, string, bool> fullTextSearch = (s, search) => s.Trim().Equals(search.Trim());

        public static string ConvertTurkishChars(this string text)
        {
            //String[] olds = { "Äž", "ÄŸ", "Ãœ", "Ã¼", "Åž", "ÅŸ", "Ä°", "Ä±", "Ã–", "Ã¶", "Ã‡", "Ã§" };
            string[] olds = { "Ğ", "ğ", "Ü", "ü", "Ş", "ş", "İ", "ı", "Ö", "ö", "Ç", "ç" };
            string[] news = { "G", "g", "U", "u", "S", "s", "I", "i", "O", "o", "C", "c" };

            for (int i = 0; i < olds.Length; i++)
                text = text.Replace(olds[i], news[i]);

            return text;
        }

        public static string ReplaceCharSet(this string text, char[] oldValues, char newValue)
        {
            var myText = new StringBuilder(text);
            foreach (var oldValue in oldValues)
            {
                if (myText.ToString().IndexOf(oldValue) > -1)
                    myText.Replace(oldValue, newValue);
            }

            return myText.ToString();
        }

        public static string GetOS(HttpRequest request)
        {
            var agentInfo = request.Headers["User-Agent"].ToString();

            if (OperatingSystem.IsWindows())
                return "Windows";
            else if (OperatingSystem.IsMacOS())
                return "MacOS";
            else if (OperatingSystem.IsLinux())
                return "Linux";
            else if (OperatingSystem.IsFreeBSD())
                return "FreeBSD";
            else if (OperatingSystem.IsAndroid())
                return "Android";
            else if (OperatingSystem.IsIOS())
                return "iOS";
            else return "Unknown OS";
        }
    }
}
