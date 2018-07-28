namespace Blog.DomainClass
{
    public abstract class Content : AuditEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string FocusKeyword { get; set; }
        public int VisitCount { get; set; }
    }
}
