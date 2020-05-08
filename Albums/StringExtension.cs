using System.Linq;

namespace Albums
{
    static class StringExtension
    {
        public static string RemoveExtraWhiteSpace(this string src) => string.Join(" ", src.Split(' ').Select(s => s.Trim()));
    }
}
