using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Repository.Context;
using Cancun.Booking.Repository.Seed;

namespace Cancun.Booking.Repository.Repositories
{
    public class SeedRepository : ISeedRepository
    {
        private readonly BookingContext _context;

        public SeedRepository(BookingContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Users.AddRangeAsync(UserSeed.Seed());
            await _context.Rooms.AddRangeAsync(RoomSeed.Seed());
            await _context.Reservations.AddRangeAsync(ReservationSeed.Seed());
            await _context.SaveChangesAsync();
        }
    }
}
