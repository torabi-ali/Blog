using PersonalBlog.Models;
using System.Collections.Generic;

namespace PersonalBlog.Extensions
{
    public static class WebExtentions
    {
        public static string ToPrepare(this ICollection<PostCategory> categories)
        {
            if (categories.Count == 0)
            {
                return null;
            }

            var result = "";
            foreach (var item in categories)
            {
                result += $"{item.CategoryId},";
            }
            result = result.Substring(0, (result.Length - 1));
            return result;
        }
    }
}
