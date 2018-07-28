namespace Blog.DomainClass
{
    public class SeoSettings : ISettings
    {
        public string MetaDataTitle { get; set; }
        public string MetaDataDescription { get; set; }
        public string MetaDataKeywords { get; set; }
    }
}