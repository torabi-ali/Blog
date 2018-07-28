using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DomainClass
{
    public class Category : Content
    {
        public string Name { get; set; }
        public string AlternativeName { get; set; }
        public int? ParentCategoryId { get; set; }

        //Navigation properties
        public virtual Category ParentCategory { get; set; }
    }

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
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

            builder.Property(p => p.Name).HasMaxLength(32).IsRequired();
            builder.Property(p => p.AlternativeName).HasMaxLength(64);

            builder.HasOne(p => p.ParentCategory).WithMany().HasForeignKey(p => p.ParentCategoryId);
        }
    }
}