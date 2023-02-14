using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Repository.Seed
{
    public static class RoomSeed
    {
        public static Room[] Seed()
        {
            return new Room[] {
                new Room { Id = 1, Name = "My Only Room" }
            };
        }
    }
}
