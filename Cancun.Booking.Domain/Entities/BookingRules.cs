
namespace Cancun.Booking.Domain.Entities
{
    public class BookingRules
    {
        public int ReservationMaxDays { get; set; }
        public int ReservationMinDaysAhead { get; set; }
        public int ReservationMaxDaysAhead { get; set; }
    }
}
