using Cancun.Booking.Domain.Interfaces;

namespace Cancun.Booking.Domain.Services
{
    public class SeedService : ISeedService
    {
        private readonly ISeedRepository _seedRepository;

        public SeedService(ISeedRepository seedRepository)
        {
            _seedRepository = seedRepository;
        }

        public async Task SeedAsync()
        {
            await _seedRepository.SeedAsync();
        }
    }
}
