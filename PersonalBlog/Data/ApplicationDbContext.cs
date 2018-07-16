using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        private readonly int? _userId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        {
        }

        #region DbSet
        public DbSet<Category> Category { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostCategory> PostCategory { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new PostCategoryConfig());
            builder.ApplyConfiguration(new CategoryConfig());
        }

        public new int SaveChanges()
        {
            try
            {
                AuditAction();
                var result = base.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                AuditAction();
                var result = await base.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                AuditAction();
                var result = await base.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void AuditAction()
        {
            var now = DateTime.Now;
            foreach (var item in ChangeTracker.Entries<AuditEntity>().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified))
            {
                var entity = item.Entity;
                switch (item.State)
                {
                    case EntityState.Added:
                        if (entity.InsertUserId.HasValue == false)
                            entity.InsertUserId = _userId;
                        entity.InsertDateTime = now;
                        break;
                    case EntityState.Deleted:
                        if (entity.DeleteUserId.HasValue == false)
                            entity.DeleteUserId = _userId;
                        entity.DeleteDateTime = now;
                        break;
                    case EntityState.Modified:
                        if (entity.UpdateUserId.HasValue == false)
                            entity.UpdateUserId = _userId;
                        entity.UpdateDateTime = now;
                        break;
                }
            }
        }
    }
}
