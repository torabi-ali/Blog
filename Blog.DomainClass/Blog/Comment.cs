using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Blog.DomainClass
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public DateTime InsertDateTime { get; set; }
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
