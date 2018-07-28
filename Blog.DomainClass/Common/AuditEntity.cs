using Microsoft.AspNetCore.Identity;
using System;

namespace Blog.DomainClass
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
        public virtual IdentityUser InsertUser { get; set; }
        public virtual IdentityUser UpdateUser { get; set; }
        public virtual IdentityUser DeleteUser { get; set; }
    }
}
