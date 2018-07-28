using Blog.DomainClass;

public static class SettingViewModel
{
    public static CommonSettings commonSettings;

    public static string WebsiteName { get; set; }
    public static string WebsiteUrl { get; set; }
    public static string FaviconPath { get; set; }
    public static string LogoPath { get; set; }
    public static string WebsitePhone { get; set; }
    public static string WebsiteEmail { get; set; }
    public static string WebsiteCompanyName { get; set; }
    public static string GoogleApiKey { get; set; }
    public static string Linkdin { get; set; }
    public static string Pinterest { get; set; }
    public static string GooglePlus { get; set; }
    public static string Youtube { get; set; }
    public static string Telegram { get; set; }
    public static string Instagram { get; set; }
    public static string Twitter { get; set; }
    public static string Facebook { get; set; }

    static SettingViewModel()
    {
        commonSettings = new CommonSettings();

        WebsiteName = commonSettings.WebsiteName;
        WebsiteUrl = commonSettings.WebsiteUrl;
        FaviconPath = commonSettings.FaviconPath;
        LogoPath = commonSettings.LogoPath;
        WebsitePhone = commonSettings.WebsitePhone;
        WebsiteEmail = commonSettings.WebsiteEmail;
        WebsiteCompanyName = commonSettings.WebsiteCompanyName;
        GoogleApiKey = commonSettings.GoogleApiKey;
        Linkdin = commonSettings.Linkdin;
        Pinterest = commonSettings.Pinterest;
        GooglePlus = commonSettings.GooglePlus;
        Youtube = commonSettings.Youtube;
        Telegram = commonSettings.Telegram;
        Instagram = commonSettings.Instagram;
        Twitter = commonSettings.Twitter;
        Facebook = commonSettings.Facebook;
    }
}