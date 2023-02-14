using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Handlers;
using Cancun.Booking.Domain.Resources.Helpers;
using Microsoft.Extensions.Options;

namespace Cancun.Booking.Domain.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly BookingRules _options;

        public AvailabilityService(IRepository<Reservation> reservationRepository,
                                   IRepository<Room> roomRepository,
                                   IOptions<BookingRules> options)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _options = options.Value;
        }

        public async Task<IEnumerable<AvailabilityDto>> GetAvailabilityAsync(int roomId)
        {
            await CheckRoomExists(roomId);

            var reservations = await _reservationRepository.GetAllByAsync(m => m.RoomId == roomId &&
                                                                          m.EndDate < DatesHelper.GetMaxReservationDate(_options.ReservationMaxDaysAhead) &&
                                                                          m.EndDate > DateTime.Now);

            var booked = reservations.SelectMany(m => DatesHelper.GetDatesBetween(m.StartDate, m.EndDate));
            var allPossibleDates = DatesHelper.GetPossibleDates(_options.ReservationMinDaysAhead, _options.ReservationMaxDaysAhead);

            return allPossibleDates.Select(m => new AvailabilityDto(m, !booked.Contains(m))).ToList();
        }

        public async Task<IEnumerable<AvailabilityDto>> GetUnavailableDates(DateTime startDate, DateTime endDate, int roomId)
        {
            var wantedDates = DatesHelper.GetDatesBetween(startDate, endDate);
            var roomAvailability = await GetAvailabilityAsync(roomId);
            return roomAvailability.Where(m => !m.IsAvailable && wantedDates.Contains(m.Date)).ToList();
        }

        private async Task CheckRoomExists(int roomId)
        {
            if (!await _roomRepository.AnyAsync(m => m.Id == roomId))
                throw new UserFriendlyException(string.Format(MessagesHelper.NotFound, nameof(Room)));
        }
    }
}
