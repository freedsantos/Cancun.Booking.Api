using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Repository.Seed
{
    public static class ReservationSeed
    {
        public static Reservation[] Seed()
        {
            return new Reservation[] {
                new Reservation { Id = 1, RoomId = 1, CreatedAt = DateTime.Now, UserId = 1, StartDate = DateTime.Now.AddDays(3).Date, EndDate = DateTime.Now.AddDays(5).Date },
                new Reservation { Id = 2, RoomId = 1, CreatedAt = DateTime.Now, UserId = 1, StartDate = DateTime.Now.AddDays(22).Date, EndDate = DateTime.Now.AddDays(24).Date },
                new Reservation { Id = 3, RoomId = 1, CreatedAt = DateTime.Now, UserId = 1, StartDate = DateTime.Now.AddDays(17).Date, EndDate = DateTime.Now.AddDays(17).Date },

                new Reservation { Id = 4, RoomId = 1, CreatedAt = DateTime.Now, UserId = 2, StartDate = DateTime.Now.AddDays(12).Date, EndDate = DateTime.Now.AddDays(14).Date },
                new Reservation { Id = 5, RoomId = 1, CreatedAt = DateTime.Now, UserId = 2, StartDate = DateTime.Now.AddDays(16).Date, EndDate = DateTime.Now.AddDays(17).Date },

                new Reservation { Id = 6, RoomId = 1, CreatedAt = DateTime.Now, UserId = 3, StartDate = DateTime.Now.AddDays(10).Date, EndDate = DateTime.Now.AddDays(11).Date },
            };
        }
    }
}
