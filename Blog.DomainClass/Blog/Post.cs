using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Blog.DomainClass
{
    public class Post : Content
    {
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public bool IsPin { get; set; }
        public int? LocationId { get; set; }

        //Navigation properties
        public virtual ICollection<PostCategory> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region ContentConfig
            builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ImagePath).HasMaxLength(256);
            builder.Property(x => x.Summary).HasMaxLength(1024);
            builder.Property(x => x.MetaKeywords).HasMaxLength(256);
            builder.Property(x => x.MetaDescription).HasMaxLength(512);
            builder.Property(x => x.FocusKeyword).HasMaxLength(32);
            #endregion

            builder.Property(p => p.SourceUrl).HasMaxLength(256);
            builder.Property(p => p.SourceName).HasMaxLength(32);
        }
    }
}
