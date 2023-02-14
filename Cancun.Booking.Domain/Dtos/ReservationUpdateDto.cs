namespace Cancun.Booking.Domain.Dtos
{
    public class ReservationUpdateDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RoomId { get; set; }
        public int UserId { get; set; }
    }
}
