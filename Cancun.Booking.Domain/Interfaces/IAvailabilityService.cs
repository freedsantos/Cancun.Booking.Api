using Cancun.Booking.Domain.Dtos;

namespace Cancun.Booking.Domain.Interfaces
{
    public interface IAvailabilityService
    {
        Task<IEnumerable<AvailabilityDto>> GetAvailabilityAsync(int roomId);
        Task<IEnumerable<AvailabilityDto>> GetUnavailableDates(DateTime startDate, DateTime endDate, int roomId);
    }
}
