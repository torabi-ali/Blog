using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PersonalBlog.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int Like { get; set; }
        public bool IsActive { get; set; }
        public bool IsOffensive { get; set; }

        public virtual Comment ParentComment { get; set; }
    }

    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text).HasMaxLength(512);
        }
    }
}
