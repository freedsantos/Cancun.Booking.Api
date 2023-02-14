using Cancun.Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cancun.Booking.Repository.Context
{
    public partial class BookingContext : DbContext
    {
        public BookingContext()
        {
        }

        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //modelBuilder.Entity<Reservation>()
            //    .Navigation(e => e.User)
            //    .AutoInclude();

            //modelBuilder.Entity<Reservation>()
            //    .Navigation(e => e.Room)
            //    .AutoInclude();

        }
    }
}
