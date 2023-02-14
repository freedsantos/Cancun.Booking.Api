namespace Cancun.Booking.Domain.Entities
{
    public class Room
    {
        public Room()
        {
            Reservations= new List<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
