using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Repository.Seed
{
    public static class UserSeed
    {
        public static User[] Seed()
        {
            return new User[] {
                new User { Id = 1, Name = "Fred Santos" },
                new User { Id = 2, Name = "Bruna Tavares" },
                new User { Id = 3, Name = "Mariane Pepe" },
                new User { Id = 4, Name = "Alexandre Tibault" }
            };
        }
    }
}
