using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;

namespace Cancun.Booking.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _userRepository.GetFirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
