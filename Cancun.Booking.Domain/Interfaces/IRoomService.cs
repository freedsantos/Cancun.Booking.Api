using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Domain.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAll();
        Task<Room> Get(int id);
    }
}
