namespace Cancun.Booking.Domain.Entities
{
    public class User
    {
        public User()
        {
            Reservations= new List<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
