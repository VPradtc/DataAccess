using System.Linq;

namespace DataAccess.Core.Extensions.System
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
