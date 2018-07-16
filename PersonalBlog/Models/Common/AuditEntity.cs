using System;

namespace PersonalBlog.Models
{
    public abstract class AuditEntity : BaseEntity
    {
        public int? InsertUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public int? DeleteUserId { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEnable { get; set; }

        //Navigation properties
        public virtual ApplicationUser InsertUser { get; set; }
        public virtual ApplicationUser UpdateUser { get; set; }
        public virtual ApplicationUser DeleteUser { get; set; }
    }
}
