using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalBlog.Models
{
    public class Setting : BaseEntity
    {
        public Setting()
        { }

        public Setting(string name, string value)
        {
            Name = name;
            Type = GetType().Name;
            Value = value;
        }

        public Setting(string name, string type, string value)
        {
            Name = name;
            Type = Type;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }


    public class SettingConfig : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Type).HasMaxLength(32);
        }
    }
}
