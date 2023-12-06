using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.Entity<Message>()
            //     .HasOne(s => s.Sender);

            // builder.Entity<Message>()
            //     .HasOne(s => s.Group);

            // builder.Entity<UserLike>()
            //     .HasOne(s => s.TargetUser)
            //     .WithMany(l => l.LikedByUsers)
            //     .HasForeignKey(s => s.TargetUserId)
            //     .OnDelete(DeleteBehavior.Cascade);

        }
    }


}