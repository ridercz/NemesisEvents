using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Altairis.NemesisEvents.DAL {

    public class NemesisEventsContext : IdentityDbContext<User, Role, int> {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"SERVER=.\SqlExpress;TRUSTED_CONNECTION=yes;DATABASE=NemesisEvents");
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Venue>().Property(x => x.Latitude).HasColumnType("decimal(18,10)").IsRequired(false);
            builder.Entity<Venue>().Property(x => x.Longitude).HasColumnType("decimal(18,10)").IsRequired(false);
            base.OnModelCreating(builder);

        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Attendee> Attendees { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<EventTag> EventTags { get; set; }

        public DbSet<UserTag> UserTags { get; set; }

        public DbSet<UserArea> UserAreas { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

    }
}