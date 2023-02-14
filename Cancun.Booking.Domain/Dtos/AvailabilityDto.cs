namespace Cancun.Booking.Domain.Dtos
{
    public class AvailabilityDto
    {
        public AvailabilityDto(DateTime date, bool isAvailable)
        {
            Date = date;
            ShortDateString = date.ToShortDateString();
            IsAvailable = isAvailable;
        }

        public DateTime Date { get; set; }
        public string ShortDateString { get; set; }
        public bool IsAvailable { get; set; }
    }
}
