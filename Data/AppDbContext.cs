using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Festival> Festivals => Set<Festival>();
        public DbSet<Stage> Stages => Set<Stage>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Performance> Performances => Set<Performance>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<AttendeeProfile> AttendeeProfiles => Set<AttendeeProfile>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== RELATIONSHIPS TO COMPLETE (exam tasks) =====
            // TODO(1:N): Festival -> Stages (required FK, cascade delete stages when festival is removed)
            // TODO(N:M w/payload): Artist <-> Stage via Performance with composite key (ArtistId, StageId, StartTime)
            // TODO(1:N): Attendee -> Tickets (required FK)
            // TODO(1:1 optional): Attendee -> AttendeeProfile (shared PK)

            // Suggested snippets (students can adapt):
            // modelBuilder.Entity<Performance>().HasKey(p => new { p.ArtistId, p.StageId, p.StartTime });
            // modelBuilder.Entity<AttendeeProfile>().HasKey(p => p.AttendeeId);
            // modelBuilder.Entity<Attendee>()
            //   .HasOne(a => a.Profile)
            //   .WithOne(p => p.Attendee)
            //   .HasForeignKey<AttendeeProfile>(p => p.AttendeeId);
        }
    }
}
