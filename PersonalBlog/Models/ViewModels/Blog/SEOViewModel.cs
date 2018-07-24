using System.Collections.Generic;

namespace PersonalBlog.Models.ViewModels
{
    public class SEOViewModel
    {
        public List<SeoMessage> TitleMessages { get; set; }
        public List<SeoMessage> KeywordMessages { get; set; }
        public List<SeoMessage> UrlMessages { get; set; }
        public List<SeoMessage> MetaDescriptionMessages { get; set; }
        public List<SeoMessage> TextMessages { get; set; }

        public int TotalScore { get; set; }
        public int SeoScore { get; set; }
        public List<SeoTopic> Topics { get; set; }
        public bool IsAcceptable { get; set; }


        public static implicit operator SEOViewModel(SeoCheker seoCheker)
        {
            return new SEOViewModel
            {
                TitleMessages = seoCheker.TitleMessages,
                KeywordMessages = seoCheker.KeywordMessages,
                UrlMessages = seoCheker.UrlMessages,
                MetaDescriptionMessages = seoCheker.MetaDescriptionMessages,
                TextMessages = seoCheker.TextMessages,
                TotalScore = seoCheker.TotalScore,
                SeoScore = seoCheker.SeoScore,
                Topics = seoCheker.Topics,
                IsAcceptable = seoCheker.IsAcceptable,
            };
        }
    }
}
