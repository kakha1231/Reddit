using Microsoft.EntityFrameworkCore;
using Reddit.Models;

namespace Reddit
{
    public class ApplcationDBContext: DbContext
    {
        public ApplcationDBContext(DbContextOptions<ApplcationDBContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Community> Communities { set; get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Community>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Communities)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict); // Or other delete behavior

            base.OnModelCreating(modelBuilder);
        }
    }
}
