using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;

namespace Cancun.Booking.Domain.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomService(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room> Get(int id)
        {
            return await _roomRepository.GetFirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
