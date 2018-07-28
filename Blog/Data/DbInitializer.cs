using Blog.DomainClass;
using Blog.Services;
using System.Linq;

namespace Blog.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            SettingService settingService = new SettingService(context);

            context.Database.EnsureCreated();

            if (context.Settings.Any())
            {
                return;
            }

            var commonSettings = new CommonSettings()
            {
                WebsiteName = "Blog",
                WebsiteUrl = "http://Blog.com",
                FaviconPath = "",
                LogoPath = "",
                WebsitePhone = "",
                WebsiteEmail = "info@blog.com",
                WebsiteCompanyName = "",
                GoogleApiKey = "",
                Linkdin = "",
                Pinterest = "",
                GooglePlus = "",
                Youtube = "",
                Telegram = "",
                Instagram = "",
                Twitter = "",
                Facebook = ""
            };

            var seoSettings = new SeoSettings()
            {
                MetaDataTitle = "The best blog",
                MetaDataDescription = "The Best way to communicate with the ones who care most about you",
                MetaDataKeywords = "blog,peronal"
            };

            settingService.SaveSetting(commonSettings);
            settingService.SaveSetting(seoSettings);
        }
    }
}
