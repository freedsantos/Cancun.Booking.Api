
namespace Cancun.Booking.Domain.Entities
{
    public class Reservation
    {
        public Reservation()
        {
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
