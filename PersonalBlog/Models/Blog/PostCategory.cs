using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalBlog.Models
{
    public class PostCategory
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Category Category { get; set; }
    }

    public class PostCategoryConfig : IEntityTypeConfiguration<PostCategory>
    {
        void IEntityTypeConfiguration<PostCategory>.Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.HasKey(p => new { p.PostId, p.CategoryId });
        }
    }
}
