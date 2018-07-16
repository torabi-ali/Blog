using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

public static class CommonUtility
{
    public static string FixUrl(this string url)
    {
        var encoded = url.EmptyIfNull()
            .RemoveStr("%", ".", ":", "~", "/", "!", "@", "#", "$", "^", "&", "*", "(", ")", "'", "+", "=", "[", "]", "{", "}", ",", ";", "/", "|", "`", "<", ">", "\"", "\\");

        var arr = encoded.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        return arr.Join("-");
    }

    public static string SuggestUrl(this string url)
    {
        var str = url
            .Replace("%", "-")
            .Replace(".", "-")
            .Replace(":", "-")
            .Replace("~", "-")
            .Replace("/", "-")
            .Replace("!", "-")
            .Replace("@", "-")
            .Replace("#", "-")
            .Replace("$", "-")
            .Replace("^", "-")
            .Replace("&", "-")
            .Replace("*", "-")
            .Replace("(", "-")
            .Replace(")", "-")
            .Replace("'", "-")
            .Replace("+", "-")
            .Replace("=", "-")
            .Replace("[", "-")
            .Replace("]", "-")
            .Replace("{", "-")
            .Replace("}", "-")
            .Replace(",", "-")
            .Replace(";", "-")
            .Replace("`", "-")
            .Replace("<", "-")
            .Replace(">", "-")
            .Replace("|", "-")
            .Replace("/", "-")
            .Replace("\\", "-")
            .Replace("\"", "-")
            .Replace("-", " ");

        var arr = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        return arr.Join("-");
    }

    public static Uri ToTiny(this Uri longUri)
    {
        var url = longUri.ToString().UrlEncode();
        var request = WebRequest.Create($"http://tinyurl.com/api-create.php?url={url}");

        var response = request.GetResponse();

        Uri returnUri = null;

        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            returnUri = new Uri(reader.ReadToEnd());
        }
        return returnUri;
    }

    public static string UrlEncode(this string url)
    {
        return HttpUtility.UrlEncode(url);
    }

    public static string UrlDecode(this string url)
    {
        return HttpUtility.UrlDecode(url);
    }

    public static string NullIfEmpty(this string str)
    {
        return (str == "" ? null : str);
    }

    public static string EmptyIfNull(this string str)
    {
        return (str == null ? "" : str);
    }

    public static string RemoveStr(this string text, params string[] strs)
    {
        foreach (var item in strs)
        {
            text = text.Replace(item, "");
        }
        return text;
    }

    public static string Fa2En(this string str)
    {
        return str
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")

                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
    }

    public static string FixPersianChars(this string str)
    {
        return str.Replace("ﮎ", "ک").Replace("ﮏ", "ک").Replace("ﮐ", "ک").Replace("ﮑ", "ک").Replace("ك", "ک").Replace("ي", "ی").Replace("ئ", "ی").Replace(" ", " ");
    }

    public static string CleanString(this string str)
    {
        return str.Trim().FixPersianChars().Fa2En().NullIfEmpty();
    }

    public static int TryToInt(this object obj)
    {
        var result = 0;
        try
        {
            result = Convert.ToInt32(obj);
        }
        catch
        {
        }
        return result;
    }

    public static object ToDBNull(this object obj)
    {
        return DBNull.Value.Equals(obj) ? null : obj;
    }

    public static byte ToByte(this object obj)
    {
        return Convert.ToByte(obj);
    }

    public static bool ToBoolean(this object obj)
    {
        return Convert.ToBoolean(obj);
    }

    public static decimal ToDecimal(this object obj)
    {
        return Convert.ToDecimal(obj);
    }

    public static int ToInt(this object obj)
    {
        return Convert.ToInt32(obj);
    }

    public static int? ToNullableInt(this object obj)
    {
        int? result = null;
        if (obj != null && obj.ToString() != "")
            result = Convert.ToInt32(obj);
        return result;
    }

    public static string ConvertEnum(string str)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        string result = string.Empty;
        foreach (var c in str)
            if (c.ToString() != c.ToString().ToLower())
                result += " " + c;
            else
                result += c.ToString();
        return result;
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }

    public static bool UrlIsLocal(this string url)
    {
        try
        {
            return new Uri(url).IsAbsoluteUri == false;
        }
        catch
        {
            return true;
        }
    }
}
