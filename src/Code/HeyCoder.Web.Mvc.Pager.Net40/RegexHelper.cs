using System.Text.RegularExpressions;

namespace HeyCoder.Web.Mvc.Pager
{
    internal class RegexHelper
    {
        internal static bool IsInt(string input)
        {
            if (Regex.IsMatch(input, @"^[0-9]+$")) return true;
            return false;
        }
    }
}
