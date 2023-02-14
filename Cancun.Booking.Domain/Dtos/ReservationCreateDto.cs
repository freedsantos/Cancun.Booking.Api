namespace Cancun.Booking.Domain.Dtos
{
    public class ReservationCreateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RoomId { get; set; }
        public int UserId { get; set; }
    }
}
