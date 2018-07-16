using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public static class SeoHelper
{
    public static string TrimStopWords(this string text, string[] stopwords)
    {
        var puretext = string.Join(" ", text.GetWords());
        stopwords.ForEach(p => puretext = string.Join(" ", Regex.Split(puretext, " " + p + " ", RegexOptions.IgnoreCase).Select(q => q.Trim())));
        return puretext;
    }

    public static string[] GetWords(this string text)
    {
        return text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static bool ContainsIgroneCase(this string text, string str)
    {
        return text.Contains(str, StringComparison.OrdinalIgnoreCase);
    }
    public static double GetWeight(this string text)
    {
        return text.Select(p => char.GetNumericValue(p)).Aggregate((p, next) => p + next);
    }

    public static IEnumerable<HtmlNode> GetElemetns(this HtmlNode node, params string[] xpath)
    {
        var list = new List<HtmlNode>();
        foreach (var item in xpath)
            list = list.Concat(node.Descendants(item)).ToList();
        return list;
    }

    public static void Add(this List<SeoMessage> seoMessages, ErrorMessage error, bool isError, params object[] values)
    {
        seoMessages.Add(new SeoMessage { Message = string.Format(error.ToDescription(), values), IsError = isError });
    }

    public static string GetSentence(this IEnumerable<string> arr)
    {
        return string.Join(", ", arr.Select(p => "\"" + p + "\""));
    }
}
public class SeoTopic
{
    public string Title { get; set; }
    public string Keyword { get; set; }
    public string MetaDescription { get; set; }
    public string Url { get; set; }
}
public class SeoMessage
{
    public string Message { get; set; }
    public bool IsError { get; set; }
}
public class SeoCheker
{
    private int TitleMinLenght = 35;//**
    private int TitleMaxLenght = 55;//**
    private int TitleWordsMinCount = 3;
    private int TitleWordsMaxCount = 12;
    //private float TitleMinDensity = 1;
    //private float TitleMaxDensity = 2;
    private float TitleMatchContentDensity = 60;//**
    private float TitleMatchMetaDescriptionDensity = 40;//**

    private int MetaDescriptionMinLenght = 50;//**
    private int MetaDescriptionMaxLenght = 155;//**
    private int MetaDescriptionWordsMinCount = 10;
    private int MetaDescriptionWordsMaxCount = 40;
    //private float MetaDescriptionKeywordMinDensity = 10;
    //private float MetaDescriptionKeywordMaxDensity = 20;
    private float MetaDescriptionStopWordsMaxDensity = 35;//**
                                                          //private int MetaDescriptionSentencesWordsMinCount = 6;
    private int MetaDescriptionSentencesWordsMaxCount = 20; //**
    private float MetaDescriptionMatchContentDensity = 60;

    private int TextMinLenght = 1500;//**
    private int TextMaxLenght = 10000;
    private int TextWordsMinCount = 300; //500 
    private int TextWordsMaxCount = 2000; //1500
    private float TextKeywordMinDensity = 1; //**
    private float TextKeywordMaxDensity = 2.5F;//5
    private float TextStopWordsMaxDensity = 30;//**
                                               //private int TextSentencesMinCount = 10;
                                               //private int TextSentencesMaxCount = 100;
                                               //private int TextSentencesMinLenght = 10;
                                               //private int TextSentencesMaxLenght = 100;
                                               //private int TextSentencesWordsMinCount = 5;
    private int TextSentencesWordsAvgCount = 20; //**
    private int TextSentencesWordsMaxCount = 35;
    private float TextSentencesWordsMaxCountDensity = 25; //**
    private int TextParagrapWordsMinCount = 40; //**
    private int TextParagrapWordsMaxCount = 150; //**
                                                 //private int TextParagraphMinCount = 1; //********************************
                                                 //private int TextParagraphMaxCount = 50; //********************************
    private int TextLinksMinCount = 1;//**
    private int TextLinksMaxCount = 100;//150
    private int TextSubheadingMinLenght = 5;
    private int TextSubheadingMaxLenght = 30; //**
    private float TextSentencesTransitionWordsMinDensity = 20; //**

    private int UrlMinLenght = 10; //**
    private int UrlMaxLenght = 40; //**
    private int UrlWordsMinCount = 2;
    private int UrlWordsMaxCount = 10;
    private float UrlStopWordsMaxDensity = 0; // max 20
    private float UrlMatchContentDensity = 60;
    private float UrlMatchMetaDescriptionDensity = 60;
    private int MinValidScore = 70;

    public static string[] StopWords = new[] {
        "یک", "درباره", "بالا", "بعد", "مجدد", "مقابل", "همه", "هستم", "و", "هر", "هست", "مثل", "در", "است", "چون", "بوده", "قبل",
        "بودن", "پایین", "میان", "هردو", "اما", "ولی", "توسط", "می توانست", "کرد", "کردن", "کرده", "انجام دادن",
        "طی", "کمی", "برای", "از", "جلوتر", "داشت", "دارد", "داره", "داشتن", "او", "می خواهد", "اینجا",
        "اینجاست", "برای او", "خودش", "مال او", "چگونه", "چطور", "من", "می توانستم", "می خواهم", "دارن", "داریم", "دارند", "دارید",
        "دارم", "اگر", "داخل", "درون", "آن", "آن ها", "هستش", "بیایید", "بیشتر", "بیشترین", "خودم",
        "نه تنها", "یکبار", "فقط", "یا", "دیگر", "خواست", "ما", "برای ما", "خودمان", "بیرون", "روی", "خاصه",
        "باید", "بنابراین", "بعضی", "مانند", "نسبت به", "ان", "آنها", "مال آنها", "خودشون", "سپس",
        "اونها", "اون", "داشتند", "می خواهند", "هستند", "این", "از طریق", "به", "خیلی", "زیر", "تا", "بودیم", "می خوایم",
        "بود", "بودین", "بودید", "داشتیم", "می خواهیم", "هستیم", "داشتین", "داشتید", "بودند", "چی", "زمانی", "زمانی که",
        "جایی", "جایی که", "بطوری که", "هنگامی که", "وقتی که", "کس", "کسی", "کسی که", "چرا", "چرا که", "همراه", "می شد", "شما",
        "کردید", "کردین", "می خواهید", "هستید", "هستین", "مال شما", "خودتون", "خودتان", "با", "کی", "را", "رو", "که", "برابر",
        //"a", "about", "above", "after", "again", "against", "all", "am", "an", "and", "any", "are", "as", "at", "be", "because", 
        //"been", "before", "being", "below", "between", "both", "but", "by", "could", "did", "do", "does", "doing", "down", 
        //"during", "each", "few", "for", "from", "further", "had", "has", "have", "having", "he", "he'd", "he'll", "he's", "her", 
        //"here", "here's", "hers", "herself", "him", "himself", "his", "how", "how's", "i", "i'd", "i'll", "i'm", "i've", "if", "in", 
        //"into", "is", "it", "it's", "its", "itself", "let's", "me", "more", "most", "my", "myself", "nor", "of", "on", "once", "only", 
        //"or", "other", "ought", "our", "ours", "ourselves", "out", "over", "own", "same", "she", "she'd", "she'll", "she's", 
        //"should", "so", "some", "such", "than", "that", "that's", "the", "their", "theirs", "them", "themselves", "then", 
        //"there", "there's", "these", "they", "they'd", "they'll", "they're", "they've", "this", "those", "through", 
        //"to", "too", "under", "until", "up", "very", "was", "we", "we'd", "we'll", "we're", "we've", "were", "what", "what's", 
        //"when", "when's", "where", "where's", "which", "while", "who", "who's", "whom", "why", "why's", "with", "would", 
        //"you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves"
        };

    private string[] TransitionWords = new[] {
            "اتفاقا", "از این جهت", "از این رو", "از جمله", "از سوی دیگر", "البته", "اگر", "اگر نه", "اگر چه", "با این حال",
            "با این وجود", "با توجه به", "با وجود آن", "با وجود این", "باز هم", "باید", "بایستی", "بر اساس آن", "بر اساس این",
            "بر این اساس", "برای مثال", "برای نمونه", "بعلاوه", "بلکه", "بنابراین", "به این دلیل", "به جهت", "به دلیل آن که",
            "به دلیل آنکه", "به رغم", "به شرطی که", "به طور خلاصه", "به طور مشابه", "به طوری که", "به عنوان مثال", "به عنوان نمونه",
            "به منظور", "به هر جهت", "به هر حال", "به همان ترتیب", "به همین ترتیب", "به ویژه", "تا آن که", "تا آنجائیکه",
            "تا آنجایی که", "تا آنکه", "تا این که", "تا اینکه", "تا زمانی که", "تا زمانیکه", "حتی", "حتی اگر", "خلاصه", "در آن صورت",
            "در ابتدا", "در انتها", "در این صورت", "در حالی است که", "در حالی که", "در حقیقت", "در صورتی که", "در ضمن", "در عین حال",
            "در غیر آن صورت", "در غیر این صورت", "در مجموع", "در مقابل", "در مورد", "در نتیجه", "در نهایت", "در هر صورت", "در واقع",
            "در پایان", "در کل", "در یک کلام", "راستی", "زیرا", "ضمنا", "علاوه بر", "علی ایحال", "لازم است", "لازم است که", "مانند",
            "مثال", "مثلا", "مسلما", "مهمتر از", "مهمتر از آن", "مهمتر از این", "مگر آن که", "مگر آنکه", "مگر این که", "مگر اینکه",
            "نه تنها", "هر چند", "همان طور که", "همانطور که", "همچنین", "همین طور که", "همینطور که", "هنوز هم", "پس از آن",
            "پس از این", "پیش از آن", "پیش از این", "چنان که", "چنانکه", "چنین که", "چون", "چون که", "چونکه", "گرچه", "یعنی", "به عبارت دیگر"
        };


    public string Title { get; set; }
    public string MetaDescription { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
    public string Keyword { get; set; }
    //public string[] Keywords { get; set; }

    public List<SeoMessage> TitleMessages { get; set; }
    public List<SeoMessage> KeywordMessages { get; set; }
    public List<SeoMessage> UrlMessages { get; set; }
    public List<SeoMessage> MetaDescriptionMessages { get; set; }
    public List<SeoMessage> TextMessages { get; set; }

    public int TitleScore { get; set; }
    public int KeywordScore { get; set; }
    public int UrlScore { get; set; }
    public int MetaDescriptionScore { get; set; }
    public int TextScore { get; set; }
    public int SumScore { get; set; }
    public int TotalScore { get; set; }
    public int TotalTitleScore { get; set; }
    public int TotalKeywordScore { get; set; }
    public int TotalUrlScore { get; set; }
    public int TotalMetaDescriptionScore { get; set; }
    public int TotalTextScore { get; set; }
    public int SeoScore { get; set; }
    public bool? KeywordDuplicated { get; set; }
    public bool? TitleDuplicated { get; set; }
    public bool? MetaDescriptionDuplicated { get; set; }
    public bool? UrlDuplicated { get; set; }
    public List<SeoTopic> Topics { get; set; }
    public bool IsAcceptable { get; set; }

    public SeoCheker(string title, string keyword, string url, string metadesc, string text, List<SeoTopic> topics = null)
    {
        TitleMessages = new List<SeoMessage>();
        KeywordMessages = new List<SeoMessage>();
        UrlMessages = new List<SeoMessage>();
        MetaDescriptionMessages = new List<SeoMessage>();
        TextMessages = new List<SeoMessage>();

        var enums = Enum.GetValues(typeof(ErrorMessage)).Cast<ErrorMessage>();
        TotalTitleScore = enums.Where(p => p.ToGetAttribute<EnumTypeAttribute>() == "Title").Count();
        TotalKeywordScore = enums.Where(p => p.ToGetAttribute<EnumTypeAttribute>() == "Keyword").Count();
        TotalUrlScore = enums.Where(p => p.ToGetAttribute<EnumTypeAttribute>() == "Url").Count();
        TotalMetaDescriptionScore = enums.Where(p => p.ToGetAttribute<EnumTypeAttribute>() == "MetaDescription").Count();
        TotalTextScore = enums.Where(p => p.ToGetAttribute<EnumTypeAttribute>() == "Text").Count();
        TotalScore = enums.Count();

        Title = (title ?? "").CleanString().EmptyIfNull();
        Keyword = (keyword ?? "").CleanString().EmptyIfNull();
        Url = (url ?? "").CleanString().EmptyIfNull();
        MetaDescription = (metadesc ?? "").CleanString().EmptyIfNull();
        Text = (text ?? "").CleanString().EmptyIfNull();

        if (topics != null)
            Topics = topics;
    }

    public void Check()
    {
        CheckKeyword();
        CheckTitle();
        CheckMetaDescription();
        CheckUrl();
        CheckText();
        SumScore = TitleScore + KeywordScore + UrlScore + MetaDescriptionScore + TextScore;
        SeoScore = Math.Ceiling(Convert.ToDecimal((decimal)SumScore / TotalScore * 100)).ToInt();
        var hasError = TitleMessages.Any(p => p.IsError) || KeywordMessages.Any(p => p.IsError) || UrlMessages.Any(p => p.IsError) || MetaDescriptionMessages.Any(p => p.IsError) || TextMessages.Any(p => p.IsError);
        IsAcceptable = hasError == false && SeoScore > MinValidScore;
    }
    public void CheckKeyword()
    {
        //-------------------------------------------- No focus keyword was set for this page. If you do not set a focus keyword, no score can be calculated.
        if (string.IsNullOrEmpty(Keyword))
            KeywordMessages.Add(ErrorMessage.KeywordIsEmpty, true);
        else
        {
            KeywordScore++;
            //-------------------------------------------- You've used this focus keyword sonce before, be sure to make very clear which URL on your site is the most important for this keyword.
            //--------------------------------------------"You've used this focus keyword times before, it's probably a good idea to read this post on cornerstone content and improve your keyword strategy.": ["شما این کلمه کلیدی کانونی را بار قبل از استفاده کرده اید . به نظر می رسد که بهتر است شما این مقاله را post on cornerstone content برای بهبود استراتژی کلمات کلیدی مطالعه کنید "],
            if ((Topics != null && Topics.Where(p => p.Keyword == Keyword).Any()) || KeywordDuplicated == true)
                KeywordMessages.Add(ErrorMessage.KeywordDuplicated, false, Keyword); //{0}Keyword
            else
                KeywordScore++;
            //-------------------------------------------- The focus keyword for this page contains one or more, consider removing them. Found.
            var aaa = StopWords.Where(p => Keyword.ContainsIgroneCase(" " + p + " "));
            if (aaa.Any())
                KeywordMessages.Add(ErrorMessage.KeywordContainStopWords, true, Keyword, aaa.GetSentence(), aaa.Count());//{0}Keyword - {1}StopWordsSentence - {2}StopWordsCount
            else
                KeywordScore++;
        }
    }
    public void CheckTitle()
    {
        //-------------------------------------------- Bad SEO score Please create a page title.
        if (string.IsNullOrEmpty(Title))
        {
            TitleMessages.Add(ErrorMessage.TitleIsEmpty, true);
        }
        else
        {
            TitleScore++;
            //-------------------------------------------- Ok SEO score The page title contains 2 characters, which is less than the recommended minimum of 35 characters. Use the space to add keyword variations or create compelling call-to-action copy.
            if (Title.Length < TitleMinLenght)
                TitleMessages.Add(ErrorMessage.TitleIsShort, true, Title, Title.Length, TitleMinLenght);//{0}Title - {1}TitleLenght - {2}TitleMinLenght
            else
                TitleScore++;
            //-------------------------------------------- Good SEO score The page title is between the 35 character minimum and the recommended 65 character maximum.
            //-------------------------------------------- The page title contains x characters, which is more than the viewable limit of x characters; some words will not be visible to users in your listing.": ["عنوان صفحه شامل %3$d کاراکتر است که از ماکزیمم تعداد کاراکتر قابل مشاهده توسط کاربران یعنی  %2$d کاراکتر بیشتر است ; بعضی کلمات توسط کاربران مشاهده نخواهد شد."],
            if (Title.Length > TitleMaxLenght)
                TitleMessages.Add(ErrorMessage.TitleIsLong, true, Title, Title.Length, TitleMaxLenght);//{0}Title - {1}TitleLenght - {2}TitleMaxLenght
            else
                TitleScore++;
            //--------------------------------------------
            var titleWords = Title.GetWords();
            if (titleWords.Length < TitleWordsMinCount)
                TitleMessages.Add(ErrorMessage.TitleWordsAreFewerThan, false, Title, titleWords.Length, TitleWordsMinCount);//{0}Title - {1}TitleWordsCount - {2}TitleWordsMinCount
            else
                TitleScore++;
            //--------------------------------------------
            if (titleWords.Length > TitleWordsMaxCount)
                TitleMessages.Add(ErrorMessage.TitleWordsAreMoreThan, false, Title, titleWords.Length, TitleWordsMaxCount);//{0}Title - {1}TitleWordsCount - {2}TitleWordsMaxCount
            else
                TitleScore++;
            //-------------------------------------------- Bad SEO score The focus keyword 'اپلیکیشن بلاگی' does not appear in the page title.
            if (Title.ContainsIgroneCase(Keyword) == false)
                TitleMessages.Add(ErrorMessage.TitleDoesNotContainFocusedKeyword, true, Title, Keyword);//{0}Title - {1}Keyword
            else
            {
                TitleScore++;
                //-------------------------------------------- Good SEO score The page title contains the focus keyword, at the beginning which is considered to improve rankings.
                //-------------------------------------------- The page title contains the focus keyword, but it does not appear at the beginning; try and move it to the beginning.
                if (Title.StartsWith(Keyword, StringComparison.OrdinalIgnoreCase) == false)
                    TitleMessages.Add(ErrorMessage.TitleDoesNotStartingFocusedKeyword, false, Title, Keyword);//{0}Title - {1}Keyword
                else
                    TitleScore++;
            }
            //--------------------------------------------
            var pureWords = Title.TrimStopWords(StopWords).GetWords();
            var matchCount = pureWords.Count(p => MetaDescription.ContainsIgroneCase(p));
            var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
            var y = Convert.ToDecimal(TitleMatchMetaDescriptionDensity.ToString("0.00"));
            if (x < y)
                TitleMessages.Add(ErrorMessage.TitleDoesNotMatchMetaDescription, false, Title, x.ToString("0.00"), y.ToString("0.00"));//{0}Title - {1}CurrentDensity - {2}MinDensity
            else
                TitleScore++;
            //--------------------------------------------
            if ((Topics != null && Topics.Where(p => p.Title == Title).Any()) || TitleDuplicated == true)
                TitleMessages.Add(ErrorMessage.TitleDuplicated, true, Title);//{0}Title
            else
                TitleScore++;
        }
    }
    public void CheckMetaDescription()
    {
        //-------------------------------------------- Bad SEO score No meta description has been specified, search engines will display copy from the page instead.
        if (string.IsNullOrEmpty(MetaDescription))
        {
            MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsEmpty, true);
        }
        else
        {
            MetaDescriptionScore++;
            //-------------------------------------------- Ok SEO score The meta description is under 120 characters, however up to 156 characters are available.
            if (MetaDescription.Length < MetaDescriptionMinLenght)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsShort, true, MetaDescription.Length, MetaDescriptionMinLenght);//{0}MetaDescLenght - {1}MetaDescriptionMinLenght
            else
                MetaDescriptionScore++;
            //-------------------------------------------- Ok SEO score The specified meta description is over 156 characters. Reducing it will ensure the entire description is visible.
            if (MetaDescription.Length > MetaDescriptionMaxLenght)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionIsLong, true, MetaDescription.Length, MetaDescriptionMaxLenght);//{0}MetaDescLenght - {1}MetaDescriptionMaxLenght
            else
                MetaDescriptionScore++;
            //--------------------------------------------
            var descWords = MetaDescription.GetWords();
            if (descWords.Length < MetaDescriptionWordsMinCount)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionWordsAreFewerThan, false, descWords.Length, MetaDescriptionWordsMinCount);//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMinCount
            else
                MetaDescriptionScore++;
            //--------------------------------------------
            if (descWords.Length > MetaDescriptionWordsMaxCount)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionWordsAreMoreThan, false, descWords.Length, MetaDescriptionWordsMaxCount); //{0}MetaDescWordsCount - {1}MetaDescriptionWordsMaxCount
            else
                MetaDescriptionScore++;
            //-------------------------------------------- Bad SEO score A meta description has been specified, but it does not contain the focus keyword. 
            //-------------------------------------------- Good SEO score The meta description contains the focus keyword.
            if (MetaDescription.ContainsIgroneCase(Keyword) == false)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDoesNotContainFocusedKeyword, true, Keyword); //{0}Keywords
            else
                MetaDescriptionScore++;
            //-------------------------------------------- Good SEO score The meta description contains no sentences over 20 words.
            var metaDescSentences = MetaDescription.Split('.', '\n');
            var aaa = metaDescSentences.Where(p => p.GetWords().Length > MetaDescriptionSentencesWordsMaxCount);
            if (aaa.Any())
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionContainSentencesMoreThanWords, false, metaDescSentences.Length, aaa.Count(), MetaDescriptionSentencesWordsMaxCount);//{0}MetaDescSentencesCount - {1}MaxWordsSentencesCount - {2}MetaDescriptionSentencesWordsMaxCount
            else
                MetaDescriptionScore++;
            //--------------------------------------------
            var stopWordsRepeats = MetaDescription.GetWords().Except(MetaDescription.TrimStopWords(StopWords).GetWords());
            var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / MetaDescription.GetWords().Length * 100);
            var y = Convert.ToDecimal(MetaDescriptionStopWordsMaxDensity.ToString("0.00"));
            if (x > y)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionContainMoreThanStopWords, false, x.ToString("0.00"), y, stopWordsRepeats.GetSentence());//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
            else
                MetaDescriptionScore++;
            //--------------------------------------------
            if ((Topics != null && Topics.Where(p => p.MetaDescription == MetaDescription).Any()) || MetaDescriptionDuplicated == true)
                MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDuplicated, true);
            else
                MetaDescriptionScore++;
        }
    }
    public void CheckUrl()
    {
        if (string.IsNullOrEmpty(Url))
        {
            UrlMessages.Add(ErrorMessage.UrlIsEmpty, true);
        }
        else
        {
            UrlScore++;
            //--------------------------------------------
            if (Url.Length < UrlMinLenght)
                UrlMessages.Add(ErrorMessage.UrlIsShort, true, Url, Url.Length, UrlMinLenght);//{0}UrlLenght - {1}UrlMinLenght
            else
                UrlScore++;
            //-------------------------------------------- The slug for this page is a bit long, consider shortening it
            if (Url.Length > UrlMaxLenght)
                UrlMessages.Add(ErrorMessage.UrlIsLong, true, Url, Url.Length, UrlMaxLenght);//{0}UrlLenght - {1}UrlMaxLenght
            else
                UrlScore++;
            //--------------------------------------------
            var url = Url.Replace('-', ' ');
            var urlWords = url.GetWords();
            if (urlWords.Length < UrlWordsMinCount)
                UrlMessages.Add(ErrorMessage.UrlWordsAreFewerThan, false, Url, urlWords.Length, UrlWordsMinCount);//{0}UrlWordsCount - {1}UrlWordsMinCount
            else
                UrlScore++;
            //--------------------------------------------
            if (urlWords.Length > UrlWordsMaxCount)
                UrlMessages.Add(ErrorMessage.UrlWordsAreMoreThan, false, Url, urlWords.Length, UrlWordsMaxCount);//{0}UrlWordsCount - {1}UrlWordsMaxCount
            else
                UrlScore++;
            //-------------------------------------------- Good SEO score The focus keyword appears in the URL for this page.
            //-------------------------------------------- Ok SEO score The focus keyword does not appear in the URL for this page. If you decide to rename the URL be sure to check the old URL 301 redirects to the new one!
            if (url.ContainsIgroneCase(Keyword) == false)
                UrlMessages.Add(ErrorMessage.UrlDoesNotContainFocusKeyword, true, Url, Keyword);//{0}Url - {1}Keyword
            else
                UrlScore++;
            //-------------------------------------------- The slug for this page contains one or more stop words, consider removing them.
            {
                var stopWordsRepeats = url.GetWords().Except(url.TrimStopWords(StopWords).GetWords());
                var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / urlWords.Length * 100);
                var y = Convert.ToDecimal(UrlStopWordsMaxDensity.ToString("0.00"));
                if (x > y)
                    UrlMessages.Add(ErrorMessage.UrlContainMoreThanStopWords, true, x.ToString("0.00"), y, stopWordsRepeats.GetSentence());//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
                else
                    UrlScore++;
            }
            //--------------------------------------------
            {
                var pureWords = url.TrimStopWords(StopWords).GetWords();
                var matchCount = pureWords.Count(p => MetaDescription.ContainsIgroneCase(p));
                var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                var y = Convert.ToDecimal(UrlMatchMetaDescriptionDensity.ToString("0.00"));
                if (x < y)
                    UrlMessages.Add(ErrorMessage.UrlDoesNotMatchMetaDescription, false, Url, x.ToString("0.00"), y.ToString("0.00"));//{0}Url - {1}CurrentDensity - {2}MinDensity
                else
                    UrlScore++;
            }
            //--------------------------------------------
            if ((Topics != null && Topics.Where(p => p.Url == Url).Any()) || UrlDuplicated == true)
                UrlMessages.Add(ErrorMessage.UrlDuplicated, true, Url);//{0}Url
            else
                UrlScore++;
        }
    }
    public void CheckText()
    {
        if (string.IsNullOrEmpty(Text))
        {
            TextMessages.Add(ErrorMessage.TextIsEmpty, true);
        }
        else
        {
            TextScore++;
            var htmlDoc = new HtmlDocument();
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.LoadHtml(Text);
            //--------------------------------------------
            var errCount = htmlDoc.ParseErrors != null ? htmlDoc.ParseErrors.Count() : 0;
            if (errCount > 0 || htmlDoc.DocumentNode == null)
                TextMessages.Add(ErrorMessage.TextHasHtmlParseError, false, errCount);//{0}HtmlParseErrorCount
            else
            {
                TextScore++;
                var innerText = htmlDoc.DocumentNode.InnerText;
                if (string.IsNullOrEmpty(innerText))
                    TextMessages.Add(ErrorMessage.TextHasHtmlNotText, false);
                else
                {
                    TextScore++;
                    //-------------------------------------------- Bad SEO score The text contains 49 words. This is far too low and should be increased.
                    if (innerText.Length < TextMinLenght)
                        TextMessages.Add(ErrorMessage.TextIsShort, true, innerText.Length, TextMinLenght); //{0}TextLenght - {1}TextMinLenght
                    else
                        TextScore++;
                    //--------------------------------------------
                    if (innerText.Length > TextMaxLenght)
                        TextMessages.Add(ErrorMessage.TextIsLong, true, innerText.Length, TextMaxLenght); //{0}TextLenght - {1}TextMaxLenght
                    else
                        TextScore++;
                    //-------------------------------------------- Good SEO score The text contains 323 words. This is more than the 300 word recommended minimum.
                    //-------------------------------------------- The text contains words, this is slightly below the word recommended minimum. Add a bit more copy.
                    var textWords = innerText.GetWords();
                    if (textWords.Length < TextWordsMinCount)
                        TextMessages.Add(ErrorMessage.TextWordsAreFewerThan, false, textWords.Length, TextWordsMinCount); //{0}TextWordsCount - {1}TextWordsMinCount
                    else
                        TextScore++;
                    //--------------------------------------------
                    if (textWords.Length > TextWordsMaxCount)
                        TextMessages.Add(ErrorMessage.TextWordsAreMoreThan, false, textWords.Length, TextWordsMaxCount); //{0}TextWordsCount - {1}TextWordsMaxCount
                    else
                        TextScore++;
                    //--------------------------------------------
                    if (innerText.ContainsIgroneCase(Keyword) == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainFocusKeyword, true, Keyword); //{0}Keyword
                    else
                    {
                        TextScore++;
                        //-------------------------------------------- The keyword density is 2.8%, which is over the advised 2.5% maximum; the focus keyword was found 144 times.
                        var keywordRepeats = Regex.Split(innerText, Keyword, RegexOptions.IgnoreCase).Length - 1;
                        var keywordDensity = Convert.ToDecimal((decimal)keywordRepeats / innerText.GetWords().Length * 100);
                        {
                            var y = Convert.ToDecimal(TextKeywordMinDensity.ToString("0.00"));
                            if (keywordDensity < y)
                                TextMessages.Add(ErrorMessage.TextKeywordDensityIsLow, true, Keyword, keywordRepeats, keywordDensity.ToString("0.00"), y.ToString("0.00"));//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MinDensity
                            else
                                TextScore++;
                        }
                        //-------------------------------------------- Bad SEO score The keyword density is 0.0%, which is a bit low; the focus keyword was found 0 times.
                        {
                            var y = Convert.ToDecimal(TextKeywordMaxDensity.ToString("0.00"));
                            if (keywordDensity > y)
                                TextMessages.Add(ErrorMessage.TextKeywordDensityIsHigh, true, Keyword, keywordRepeats, keywordDensity.ToString("0.00"), y.ToString("0.00"));//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MaxDensity
                            else
                                TextScore++;
                        }
                    }
                    //--------------------------------------------
                    {
                        var stopWordsRepeats = innerText.GetWords().Except(innerText.TrimStopWords(StopWords).GetWords());
                        var x = Convert.ToDecimal((decimal)stopWordsRepeats.Count() / textWords.Length * 100);
                        var y = Convert.ToDecimal(TextStopWordsMaxDensity.ToString("0.00"));
                        if (x > y)
                            TextMessages.Add(ErrorMessage.TextContainMoreThanStopWords, false, x.ToString("0.00"), y, stopWordsRepeats.GetSentence()); //{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- sure each page has a unique H1 tag
                    var headings = htmlDoc.DocumentNode.GetElemetns("h1");
                    if (headings.Any() == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainHeading, true);
                    else
                    {
                        TextScore++;
                        //--------------------------------------------
                        if (headings.Count() > 1)
                            TextMessages.Add(ErrorMessage.TextContainMultipleHeading, true, headings.Count()); //{0}H1Count
                        else
                            TextScore++;
                        //--------------------------------------------
                        if (headings.FirstOrDefault(p => p.InnerText.ContainsIgroneCase(Keyword)) == null)
                            TextMessages.Add(ErrorMessage.TextFirstHeadingDoesContainKeyword, false, Keyword); //{0}Keyword
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- Ok SEO score No subheading tags (like an H2) appear in the copy.
                    //-------------------------------------------- Bad SEO score The text does not contain any subheadings. Add at least one subheading.
                    var subheadings = htmlDoc.DocumentNode.GetElemetns("h2", "h3", "h4", "h5", " h6");
                    if (subheadings.Any() == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainAnySubheadings, true);
                    else
                    {
                        TextScore++;
                        //-------------------------------------------- 
                        if (subheadings.Where(p => p.InnerText.ContainsIgroneCase(Keyword)).Any() == false)
                            TextMessages.Add(ErrorMessage.TextSubHeadingsDoesContainKeyword, false, Keyword); //{0}Keyword
                        else
                            TextScore++;
                        //-------------------------------------------- 
                        {
                            var aaa = subheadings.Where(p => p.InnerText.Length < TextSubheadingMinLenght).Select(p => p.InnerText);
                            aaa = aaa.Concat(headings.Where(p => p.InnerText.Length < TextSubheadingMinLenght).Select(p => p.InnerText));
                            if (aaa.Any())
                                TextMessages.Add(ErrorMessage.TextSubHeadingsContainFewerThanCharacters, false, aaa.Count(), TextSubheadingMinLenght, aaa.GetSentence());//{0}HeadAndSubheadingsAreFewerLenght - {1}TextSubheadingMinLenght - {2}0Sentences
                            else
                                TextScore++;
                        }
                        //-------------------------------------------- 
                        {
                            var aaa = subheadings.Where(p => p.InnerText.Length > TextSubheadingMaxLenght).Select(p => p.InnerText);
                            aaa = aaa.Concat(headings.Where(p => p.InnerText.Length > TextSubheadingMaxLenght).Select(p => p.InnerText));
                            if (subheadings.Any(p => p.InnerText.Length > TextSubheadingMaxLenght) || headings.Any(p => p.InnerText.Length > TextSubheadingMaxLenght))
                                TextMessages.Add(ErrorMessage.TextSubHeadingsContainMoreThanCharacters, false, aaa.Count(), TextSubheadingMaxLenght, aaa.GetSentence());//{0}HeadAndSubheadingsAreMoreLenght - {1}TextSubheadingMaxLenght - {2}0Sentences
                            else
                                TextScore++;
                        }
                    }
                    //--------------------------------------------
                    var pTags = htmlDoc.DocumentNode.GetElemetns("p"); //.SelectNodes("//p");
                    if (pTags.Any() == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainAnyParagraph, false);
                    else
                    {
                        TextScore++;
                        //-------------------------------------------- Bad SEO score The focus keyword doesn't appear in the first paragraph of the copy. Make sure the topic is clear immediately.
                        //-------------------------------------------- The focus keyword appears in the first paragraph of the copy.
                        if (pTags.First().InnerText.ContainsIgroneCase(Keyword) == false)
                            TextMessages.Add(ErrorMessage.FirstParagraphDoesNotContainFocusedKeyword, false, Keyword);//{0}Keyword
                        else
                            TextScore++;
                        //-------------------------------------------- Ok SEO score 5 of the paragraphs contain less than the recommended minimum of 40 words. Try to expand these paragraphs, or connect each of them to the previous or next paragraph.
                        var pWords = pTags.Select(p => p.InnerText.GetWords().Length).ToList();
                        {
                            var aaa = pWords.Where(p => p < TextParagrapWordsMinCount);
                            if (aaa.Any())
                                TextMessages.Add(ErrorMessage.ParagraphWordsAreFewerThan, false, aaa.Count(), TextParagrapWordsMinCount);//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMinCount
                            else
                                TextScore++;
                        }
                        //--------------------------------------------
                        {
                            var aaa = pWords.Where(p => p > TextParagrapWordsMaxCount);
                            if (aaa.Any())
                                TextMessages.Add(ErrorMessage.ParagraphWordsAreMoreThan, false, aaa.Count(), TextParagrapWordsMaxCount);//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMaxCount
                            else
                                TextScore++;
                        }
                    }
                    //-------------------------------------------- Try to make shorter sentences to improve readability.
                    //-------------------------------------------- Try to make shorter sentences, using less difficult words to improve readability.
                    var sentences = htmlDoc.DocumentNode.GetElemetns("p", "div").Select(p => p.InnerText.Split(new[] { ".", "<br>", "<br/>", "<br />" }, StringSplitOptions.None)).SelectMany(p => p);
                    var sentencesWords = sentences.Select(p => p.GetWords().Length).ToList();
                    //if (sentences.Where(p => p.Length < TextSentencesMinLenght).Any())
                    //    TextMessages.Add(SeoMessage.SentenceIsShort);
                    //else
                    //    TextScore++;
                    ////--------------------------------------------
                    //if (sentences.Where(p => p.Length > TextSentencesMaxLenght).Any())
                    //    TextMessages.Add(SeoMessage.SentenceIsLong);
                    //else
                    //    TextScore++;
                    //-------------------------------------------- 
                    {
                        var aaa = sentencesWords.Where(p => p > TextSentencesWordsMaxCount);
                        if (aaa.Any())
                            TextMessages.Add(ErrorMessage.TextSentencesContainMoreThanWords, false, aaa.Count(), TextSentencesWordsMaxCount); //{0}MoreWordsSetencesCount - {1}TextSentencesWordsMaxCount
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- Bad SEO score 50% of the sentences contain more than 20 words, which is more than the recommended maximum of 25%. Try to shorten your sentences.
                    {
                        var scountmore = sentencesWords.Count(p => p > TextSentencesWordsAvgCount);
                        var x = Convert.ToDecimal((decimal)scountmore / sentencesWords.Count * 100);
                        var y = Convert.ToDecimal(TextSentencesWordsMaxCountDensity.ToString("0.00"));
                        if (sentencesWords.Any() && x > y)
                            TextMessages.Add(ErrorMessage.TextAvgSentencesAreMoreThan, false, scountmore, TextSentencesWordsAvgCount, x.ToString("0.00"), y.ToString("0.00")); //{0}MoreWordsSetencesCount - {1}TextSentencesWordsAvgCount - {2}CurrentDensity - {3}MaxDensity
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- Bad SEO score 0.0% of the sentences contain a transition word or phrase, which is less than the recommended minimum of 20%.
                    {
                        var transitionSentences = sentences.Where(p => TransitionWords.Where(q => p.ContainsIgroneCase(q)).Any());
                        var x = Convert.ToDecimal((decimal)transitionSentences.Count() / sentences.Count() * 100);
                        var y = Convert.ToDecimal(TextSentencesTransitionWordsMinDensity.ToString("0.00"));
                        if (sentences.Any() && x < y)
                            TextMessages.Add(ErrorMessage.TransitionSentencesAreFewerThan, false, transitionSentences.Count(), x.ToString("0.00"), y.ToString("0.00"));//{0}TransitionSentencesCount - {1}CurrentDensity - {2}MinDensity
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- Bad SEO score No images appear in this page, consider adding some as appropriate.
                    var images = htmlDoc.DocumentNode.GetElemetns("img").Where(p => p.Attributes["src"] != null).ToList();
                    if (images.Any() == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainAnyImage, false);
                    else
                    {
                        TextScore++;
                        var altImages = images.Select(p => p.GetAttributeValue("alt", ""));
                        //-------------------------------------------- Ok SEO score The images on this page are missing alt attributes.
                        {
                            var aaa = altImages.Count(p => p == "");
                            if (aaa > 0)
                                TextMessages.Add(ErrorMessage.ImagesDoesNotContainAltAttribute, true, aaa, images.Count); //{0}NoAltCount - {1}ImageCount
                            else
                                TextScore++;
                        }
                        //-------------------------------------------- The images on this page do not have alt tags containing your focus keyword.
                        if (altImages.Where(p => p.ContainsIgroneCase(Keyword)).Any() == false)
                            TextMessages.Add(ErrorMessage.NoImageContainKeywordInAltAttribute, true, Keyword, images.Count); //{0}Keyword - {1}ImageCount
                        else
                            TextScore++;
                        //-------------------------------------------- It's also a best practice to use keywords in the file names of images
                        if (altImages.Where(p => (p.NullIfEmpty() ?? Path.GetFileNameWithoutExtension(p)).EmptyIfNull().ContainsIgroneCase(Keyword)).Any() == false)
                            TextMessages.Add(ErrorMessage.NoImageContainKeywordInFielName, false, Keyword, images.Count); //{0}Keyword - {1}ImageCount
                        else
                            TextScore++;
                    }
                    //-------------------------------------------- Ok SEO score No links appear in this page, consider adding some as appropriate.
                    var links = htmlDoc.DocumentNode.GetElemetns("a").Select(p => p.GetAttributeValue("href", "")).Where(p => p != "");
                    if (links.Any() == false)
                        TextMessages.Add(ErrorMessage.TextDoesNotContainAnyLinks, false);
                    else
                    {
                        TextScore++;
                        var linkCount = links.Count();
                        //--------------------------------------------
                        if (linkCount < TextLinksMinCount)
                            TextMessages.Add(ErrorMessage.TextLinksAreFewerThan, false, linkCount, TextLinksMinCount); //{0}LinkCount - {1}MinCount
                        else
                            TextScore++;
                        //--------------------------------------------{
                        if (linkCount > TextLinksMaxCount)
                            TextMessages.Add(ErrorMessage.TextLinksAreMoreThan, false, linkCount, TextLinksMaxCount); //{0}LinkCount - {1}MaxCount
                        else
                            TextScore++;
                        //--------------------------------------------
                        //Internal links on your own site pointing to page have keywords in anchor text, including breadcrumbs and navigational links (if possible)
                        if (links.Where(p => p.UrlIsLocal() == true).Any() == false)
                            TextMessages.Add(ErrorMessage.TextDoesNotContainAnyInboundLinks, false, linkCount); //{0}LinkCount
                        else
                            TextScore++;
                        //-------------------------------------------- No outbound links appear in this page, consider adding some as appropriate.
                        if (links.Where(p => p.UrlIsLocal() == false).Any() == false)
                            TextMessages.Add(ErrorMessage.TextDoesNotContainAnyOutboundLinks, false, linkCount); //{0}LinkCount
                        else
                            TextScore++;
                    }
                    if (string.IsNullOrEmpty(MetaDescription) == false)
                    {
                        //--------------------------------------------
                        var pureWords = MetaDescription.TrimStopWords(StopWords).GetWords();
                        var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                        var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                        var y = Convert.ToDecimal(MetaDescriptionMatchContentDensity.ToString("0.00"));
                        if (pureWords.Length > 0 && x < y)
                            MetaDescriptionMessages.Add(ErrorMessage.MetaDescriptionDoesNotMatchContent, true, x.ToString("0.00"), y.ToString("0.00")); //{0}CurrentDensity - {1}MinDensity
                        else
                            TextScore++;
                    }
                    if (string.IsNullOrEmpty(Title) == false)
                    {
                        //--------------------------------------------
                        var pureWords = Title.TrimStopWords(StopWords).GetWords();
                        var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                        var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                        var y = Convert.ToDecimal(TitleMatchContentDensity.ToString("0.00"));
                        if (x < y)
                            TitleMessages.Add(ErrorMessage.TitleDoesNotMatchContent, true, Title, x.ToString("0.00"), y.ToString("0.00")); //{0}Title - {1}CurrentDensity - {2}MinDensity
                        else
                            TextScore++;
                    }
                    if (string.IsNullOrEmpty(Url) == false)
                    {
                        //--------------------------------------------
                        var pureWords = Url.Replace('-', ' ').TrimStopWords(StopWords).GetWords();
                        var matchCount = pureWords.Count(p => innerText.ContainsIgroneCase(p));
                        var x = Convert.ToDecimal((decimal)matchCount / pureWords.Length * 100);
                        var y = Convert.ToDecimal(UrlMatchContentDensity.ToString("0.00"));
                        if (x < y)
                            UrlMessages.Add(ErrorMessage.UrlDoesNotMatchContent, true, Url, x.ToString("0.00"), y.ToString("0.00")); //{0}Url - {1}CurrentDensity - {2}MinDensity
                        else
                            TextScore++;
                    }
                }
            }
        }
    }
}
public enum ErrorMessage
{
    //Title
    [EnumTypeAttribute("Title"), Description("لطفا یک عنوان برای صفحه وارد کنید.")]
    TitleIsEmpty,
    [EnumTypeAttribute("Title"), Description("عنوان صفحه {1} کاراکتر دارد که کمتر از مقدار پیشنهادی {2} کاراکتر است.")]//{0}Title - {1}TitleLenght - {2}TitleMinLenght
    TitleIsShort,
    [EnumTypeAttribute("Title"), Description("عنوان صفحه {1} کاراکتر دارد که بیشتر از مقدار پیشنهادی {2} کاراکتر است.")]//{0}Title - {1}TitleLenght - {2}TitleMaxLenght
    TitleIsLong,
    [EnumTypeAttribute("Title"), Description("کلمه کلیدی در عنوان صفحه لحاظ نشده است.")]//{0}Title - {1}Keyword
    TitleDoesNotContainFocusedKeyword,
    [EnumTypeAttribute("Title"), Description("عنوان صفحه شامل کلمه کلیدی هست، اما در ابتدای عنوان قرار نگرفته، لطفا آن را ابتدای عنوان قرار دهید.")]//{0}Title - {1}Keyword
    TitleDoesNotStartingFocusedKeyword,
    [EnumTypeAttribute("Title"), Description("تعداد کلمات عنوان {1} عدد است که این بیشتر از مقدار پیشنهادی {2} عدد است.")]//{0}Title - {1}TitleWordsCount - {2}TitleWordsMaxCount
    TitleWordsAreMoreThan,
    [EnumTypeAttribute("Title"), Description("تعداد کلمات عنوان {1} عدد است که این کمتر از مقدار پیشنهادی {2} عدد است.")]//{0}Title - {1}TitleWordsCount - {2}TitleWordsMinCount
    TitleWordsAreFewerThan,
    [EnumTypeAttribute("Title"), Description("عنوان صفحه تکراری است و در پست های قبلی استفاده شده است.")]//{0}Title
    TitleDuplicated,
    [EnumTypeAttribute("Title"), Description("میزان شباهت عنوان با متاتگ توضیحات {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Title - {1}CurrentDensity - {2}MinDensity
    TitleDoesNotMatchMetaDescription,
    //TitleContainStopWords,
    [EnumTypeAttribute("Title"), Description("میزان شباهت عنوان با متن {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")] //{0}Title - {1}CurrentDensity - {2}MinDensity
    TitleDoesNotMatchContent,

    //Keyword
    [EnumTypeAttribute("Keyword"), Description("کلمه کلیدی برای این صفحه تعیین نشده است. لطفا یک کلمه کلیدی پر مخاطب که در متن شما وجود دارد را انتخاب کنید.")]
    KeywordIsEmpty,
    [EnumTypeAttribute("Keyword"), Description("کلمه کلیدی \"{0}\" تکراری است و در پست های قبلی استفاده شده است.")]//{0}Keyword
    KeywordDuplicated,
    [EnumTypeAttribute("Keyword"), Description("کلمه کلیدی شامل {2} کلمه خیلی عمومی {1} است. آنها را پاک کنید.")]//{0}Keyword - {1}StopWordsSentence - {2}StopWordsCount
    KeywordContainStopWords,


    //MetaDescription
    [EnumTypeAttribute("MetaDescription"), Description("متای توضیحات مشخص نشده است. موتور های جستجو قسمتی از متن صفحه را به جای آن نشان خواهند داد.")]
    MetaDescriptionIsEmpty,
    [EnumTypeAttribute("MetaDescription"), Description("متا تگ توضیحات تعریف شده است اما کلمه کلیدی \"{0}\" در آن دیده نمی شود.")] //{0}Keywords
    MetaDescriptionDoesNotContainFocusedKeyword,
    [EnumTypeAttribute("MetaDescription"), Description("{1} جمله در متاتگ توضیحات بیش از {2} کلمه دارد. لطفا آن را کاهش دهید.")]//{0}MetaDescSentencesCount - {1}MaxWordsSentencesCount - {2}MetaDescriptionSentencesWordsMaxCount
    MetaDescriptionContainSentencesMoreThanWords,
    [EnumTypeAttribute("MetaDescription"), Description("متاتگ توضیحات {0} کاراکتر دارد که این کمتر از مقدار پیشنهادی {1} کاراکتر است.")]//{0}MetaDescLenght - {1}MetaDescriptionMinLenght
    MetaDescriptionIsShort,
    [EnumTypeAttribute("MetaDescription"), Description("متاتگ توضیحات {0} کاراکتر دارد که این بیشتر از مقدار پیشنهادی {1} کاراکتر است.")]//{0}MetaDescLenght - {1}MetaDescriptionMaxLenght
    MetaDescriptionIsLong,
    [EnumTypeAttribute("MetaDescription"), Description("متاتگ توضیحات شامل {0} کلمه است که این بیشتر از مقدار پیشنهادی {1} است.")]//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMaxCount
    MetaDescriptionWordsAreMoreThan,
    [EnumTypeAttribute("MetaDescription"), Description("متاتگ توضیحات شامل {0} کلمه است که این کمتر از مقدار پیشنهادی {1} است.")]//{0}MetaDescWordsCount - {1}MetaDescriptionWordsMinCount
    MetaDescriptionWordsAreFewerThan,
    [EnumTypeAttribute("MetaDescription"), Description("تراکم کلمات خیلی عمومی {2} متاتگ توضیحات {0}% است که این مقدار بیش از مقدار پیشنهادی {1}% است")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
    MetaDescriptionContainMoreThanStopWords,
    [EnumTypeAttribute("MetaDescription"), Description("میزان شباهت متاتگ توضیحات با متن صفحه {0}% است که این کمتر از مقدار پیشنهادی {1}% است.")] //{0}CurrentDensity - {1}MinDensity
    MetaDescriptionDoesNotMatchContent,
    [EnumTypeAttribute("MetaDescription"), Description("متاتگ توضیحات تکراری است و در پست های قبلی استفاده شده است")]
    MetaDescriptionDuplicated,

    //Text
    [EnumTypeAttribute("Text"), Description("متن خالی است، لطفا متن را وارد کنید.")]
    TextIsEmpty,
    [EnumTypeAttribute("Text"), Description("متن {0} خطای html ایی دارد.")]//{0}HtmlParseErrorCount
    TextHasHtmlParseError,
    [EnumTypeAttribute("Text"), Description("متن شامل تگ های html است ولی محتوا ندارد.")]
    TextHasHtmlNotText,
    [EnumTypeAttribute("Text"), Description("متن {0} کاراکتر دارد که این کمتر از حداقل مقدار پیشنهادی {1} است.")]//{0}TextLenght - {1}TextMinLenght
    TextIsShort,
    [EnumTypeAttribute("Text"), Description("متن {0} کاراکتر دارد که این بیشتر از مقدار پیشنهادی {1} است.")] //{0}TextLenght - {1}TextMaxLenght
    TextIsLong,
    [EnumTypeAttribute("Text"), Description("متن شامل کلمه کلیدی کانونی نیست.")] //{0}Keyword
    TextDoesNotContainFocusKeyword,
    [EnumTypeAttribute("Text"), Description("متن شامل تگ تیتری(h1) نیست.")]
    TextDoesNotContainHeading,
    [EnumTypeAttribute("Text"), Description("متن شامل {0} تگ تیتری(h1) است، آنرا به یک عدد کاهش دهید.")] //{0}H1Count
    TextContainMultipleHeading,
    [EnumTypeAttribute("Text"), Description("هیچ تگ زیرتیتری مانند (H2,H3,...) در نوشته‌ی شما نیست.")]
    TextDoesNotContainAnySubheadings,
    [EnumTypeAttribute("Text"), Description("اولین زیر تیتر شامل کلمه کلیدی \"{0}\" نیست")] //{0}Keyword
    TextFirstHeadingDoesContainKeyword,
    [EnumTypeAttribute("Text"), Description("شما در هیچ کدام از زیرتیتر ها (H2,H3,...) .کلمه کلیدی \"{0}\" استفاده نکرده اید")] //{0}Keyword
    TextSubHeadingsDoesContainKeyword,
    [EnumTypeAttribute("Text"), Description("{0} تیتر/زیرتیتر در متن وجود دارد که کمتر از {1} کارکتر دارد.")]//{0}HeadAndSubheadingsAreFewerLenght - {1}TextSubheadingMinLenght - {2}0Sentences
    TextSubHeadingsContainFewerThanCharacters,
    [EnumTypeAttribute("Text"), Description("{0} تیتر/زیرتیتر در متن وجود دارد که بیشتر از {1} کارکتر دارد.")]//{0}HeadAndSubheadingsAreMoreLenght - {1}TextSubheadingMaxLenght - {2}0Sentences
    TextSubHeadingsContainMoreThanCharacters,
    [EnumTypeAttribute("Text"), Description("هیچ پاراگرافی (تگ p یا div) در متن دیده نمی شود.")]
    TextDoesNotContainAnyParagraph,
    [EnumTypeAttribute("Text"), Description("هیچ لینکی (تگ a) در متن دیده نمی شود.")]
    TextDoesNotContainAnyLinks,
    [EnumTypeAttribute("Text"), Description("{0} لینک در متن وجود دارد که این مقدار کمتر از حداقل مقدار پیشنهادی {1} است.")]//{0}LinkCount - {1}MinCount
    TextLinksAreFewerThan,
    [EnumTypeAttribute("Text"), Description("{0} لینک در متن وجود دارد که این مقدار بیشتر از مقدار پیشنهادی {1} است.")]//{0}LinkCount - {1}MaxCount
    TextLinksAreMoreThan,
    [EnumTypeAttribute("Text"), Description("در این متن، لینک داخلی (لینک به سایت خودتان) وجود ندارد! یک یا چندتا لینک داخلی مرتبط با پست های سایت به نوشته اضافه کنید.")]//{0}LinkCount
    TextDoesNotContainAnyInboundLinks,
    [EnumTypeAttribute("Text"), Description("در این متن، لینک خارجی (لینک به سایت دیگری) وجود ندارد! یک یا چندتا لینک خارجی مرتبط با محتوای نوشته به سایت‌های معتبر اضافه کنید.")]//{0}LinkCount
    TextDoesNotContainAnyOutboundLinks,
    [EnumTypeAttribute("Text"), Description("تراکم کلمه کلیدی در متن {2}% است که این مقدار کمتر از مقدار پیشنهادی {3}% است. کلمه کلیدی شما {1} بار در متن تکرار شده است")]//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MinDensity
    TextKeywordDensityIsLow,
    [EnumTypeAttribute("Text"), Description("تراکم کلمه کلیدی در متن {2}% است که این مقدار بیشتر از مقدار پیشنهادی {3}% است. کلمه کلیدی شما {1} بار در متن تکرار شده است")]//{0}Keyword - {1}KeywordRepeatCount - {2}CurrentDensity - {3}MaxDensity
    TextKeywordDensityIsHigh,
    [EnumTypeAttribute("Text"), Description("تعداد کلمات این متن {0} عدد است که این مقدار کمتر از حداقل  مقدار پیشنهادی {1} عدد است.")]//{0}TextWordsCount - {1}TextWordsMinCount
    TextWordsAreFewerThan,
    [EnumTypeAttribute("Text"), Description("تعداد کلمات این متن {0} عدد است که این مقدار بیشتر از حداکثر  مقدار پیشنهادی {1} عدد است.")]//{0}TextWordsCount - {1}TextWordsMaxCount
    TextWordsAreMoreThan,
    [EnumTypeAttribute("Text"), Description("تراکم کلمات خیلی عمومی ({2}) این متن {0} عدد است که این مقدار بیشتر از حداکثر  مقدار پیشنهادی {1} عدد است.")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence
    TextContainMoreThanStopWords,
    [EnumTypeAttribute("Text"), Description("تراکم کلمات گذار (Transition Words) موجود در متن {1}% است که این مقدار کمتر از حداقل پیشنهادی {2}% است. {0} جمله در متن شامل کلمه گذار می باشد.")]//{0}TransitionSentencesCount - {1}CurrentDensity - {2}MinDensity
    TransitionSentencesAreFewerThan,
    //TextContainIllegalCharacters

    //Sentence
    //SentenceIsShort,
    //[EnumTypeAttribute("Text"), Description("سعی کنید برای خوانایی بیشتر جمله ها رو کوتاه تر کنید.")]
    //SentenceIsLong,
    [EnumTypeAttribute("Text"), Description("{0} جمله در متن شامل بیش از {1} کلمه است که یعنی {2}% از جملات متن، و این بیش از حداکثر مقدار پیشنهادی {3}% است.")]//{0}MoreWordsSetencesCount - {1}TextSentencesWordsAvgCount - {2}CurrentDensity - {3}MaxDensity
    TextAvgSentencesAreMoreThan,
    [EnumTypeAttribute("Text"), Description("{0} جمله در متن شامل بیش از حداکثر مقدار پیشنهادی {1} کلمه است.")]//{0}MoreWordsSetencesCount - {1}TextSentencesWordsMaxCount
    TextSentencesContainMoreThanWords,
    //SentenceVariationIsBad, ********************
    //combination of long and short sentences

    //Paragraph
    [EnumTypeAttribute("Text"), Description("{0} از پاراگراف (تگ p یا div) های موجود در متن بیشتر از حداکثر {1} کلمه پیشنهادی است.")]//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMaxCount
    ParagraphWordsAreMoreThan,
    [EnumTypeAttribute("Text"), Description("{0} از پاراگراف (تگ p یا div) های موجود در متن کمتر از حداقل {1} کلمه پیشنهادی است.")]//{0}ParagrapMoreWordsCount - {1}TextParagrapWordsMinCount
    ParagraphWordsAreFewerThan,
    [EnumTypeAttribute("Text"), Description("کلمه کلیدی \"{0}\" در پاراگراف اول دیده نمی شود.")]//{0}Keyword
    FirstParagraphDoesNotContainFocusedKeyword,

    //Image
    [EnumTypeAttribute("Text"), Description("در این نوشته تصویری مشاهده نمی شود. شاید بهتر باشد تصویری اضافه نمایید.")]
    TextDoesNotContainAnyImage,
    //ImageSrcIsEmptyOrNotFound,
    //ImageNotOptimized,
    //ImageSizeIsOverThan,
    [EnumTypeAttribute("Text"), Description("{0} تصویر از مجموع {1} تصویر موجود در متن، برچسب (alt) ندارد.")]//{0}NoAltCount - {1}ImageCount
    ImagesDoesNotContainAltAttribute,
    [EnumTypeAttribute("Text"), Description("هیچکدام از {1} تصویر استفاده شده در این متن، شامل کلمه کلیدی \"{0}\" در برچسب (alt) نیستند.")]//{0}Keyword - {1}ImageCount
    NoImageContainKeywordInAltAttribute,
    [EnumTypeAttribute("Text"), Description("هیچکدام از {1} تصویر استفاده شده در این متن، شامل کلمه کلیدی \"{0}\" در نام فایل (src) نیستند.")]//{0}Keyword - {1}ImageCount
    NoImageContainKeywordInFielName,

    //Url
    [EnumTypeAttribute("Url"), Description("آدرس صفحه خالی است، لطفا یک آدرس مناسب وارد کنید.")]
    UrlIsEmpty,
    [EnumTypeAttribute("Url"), Description("آدرس صفحه شامل {0} کاراکتر است که این مقدار کمتر از حداقل مقدار پیشنهادی {1} کاراکتر است")]//{0}UrlLenght - {1}UrlMinLenght
    UrlIsShort,
    [EnumTypeAttribute("Url"), Description("آدرس صفحه شامل {0} کاراکتر است که این مقدار بیشتر از حداکثر مقدار پیشنهادی {1} کاراکتر است")]//{0}UrlLenght - {1}UrlMaxLenght
    UrlIsLong,
    [EnumTypeAttribute("Url"), Description("کلمه کلیدی \"{1}\" در آدرس این صفحه لحاظ نشده است.")]//{0}Url - {1}Keyword
    UrlDoesNotContainFocusKeyword,
    [EnumTypeAttribute("Url"), Description("آدرس صفحه شامل {0} کلمه است که این مقدار بیشتر از حداکثر مقدار پیشنهادی {1} کلمه است")]//{0}UrlWordsCount - {1}UrlWordsMaxCount
    UrlWordsAreMoreThan,
    [EnumTypeAttribute("Url"), Description("آدرس صفحه شامل {0} کلمه است که این مقدار کمتر از حداقل مقدار پیشنهادی {1} کلمه است")]//{0}UrlWordsCount - {1}UrlWordsMinCount
    UrlWordsAreFewerThan,
    [EnumTypeAttribute("Url"), Description("آدرس این صفحه شامل یک یا بیشتر کلمه خیلی عمومی {2} است، لطفا آنها را حذف کنید.")]//{0}CurrentDensity - {1}MaxDensity - {2}StopWordsSentence "آدرس این صفحه شامل {0}% تراکم کلمه خیلی عمومی {2} است ، لطفا آنها را حذف کنید."
    UrlContainMoreThanStopWords,
    [EnumTypeAttribute("Url"), Description("میزان شباهت آدرس با متاتگ توضیحات {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Url - {1}CurrentDensity - {2}MinDensity
    UrlDoesNotMatchMetaDescription,
    [EnumTypeAttribute("Url"), Description("آدرس صفحه تکراری است و در پست های قبلی استفاده شده است")]//{0}Url
    UrlDuplicated,
    [EnumTypeAttribute("Url"), Description("میزان شباهت آدرس با متن {1}% است که این کمتر از مقدار پیشنهادی {2}% است.")]//{0}Url - {1}CurrentDensity - {2}MinDensity
    UrlDoesNotMatchContent,
}

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class EnumTypeAttribute : Attribute, IAttribute
{
    public string Name { get; set; }
    public EnumTypeAttribute(string type)
    {
        Name = type;
    }
}

//Good SEO score The sentence variation score is 3.06, which is above the recommended minimum of 3. The text contains a nice combination of long and short sentences.
//Good SEO score 0% of the words contain over 4 syllables, which is less than or equal to the recommended maximum of 10%.
//Good SEO score The copy scores 100 in the Flesch Reading Ease test, which is considered very easy to read.
//-------------------------------------------- 
//"This page has %1$s outbound link(s).": ["این صفحه شامل %1$s  لینک خروجی است."],
//"This page has %2$s outbound link(s), all nofollowed.": [""],
//"This page has %2$s nofollowed link(s) and %3$s normal outbound link(s).": ["این صفحه %2$s  لینک به صفحه بیرونی دارد که توسط موتور های جستجو دنبال نمی شوند."],
//"You're linking to another page with the focus keyword you want this page to rank for. Consider changing that if you truly want this page to rank.": ["شما با کلمه کلیدی کانونی که می خواهید در این صفحه به خاطر آن رتبه داشته باشید ،به صفحه دیگری لینک داده اید. در صورتی که می خواهید در این صفحه رتبه خوبی داشته باشید آن را تغییر دهید."],
//"Your keyphrase is over 10 words, a keyphrase should be shorter.": ["عبارت کلیدی بیش از 10 کلمه است ، عبارت کلیدی باید کوتاه تر باشد"],
//Bad SEO score 1 of the subheadings is followed by less than the recommended minimum of 40 words. Consider deleting that particular subheading, or the following subheading.
//Best practice is to open external links in a new tab/window
//use anchor text