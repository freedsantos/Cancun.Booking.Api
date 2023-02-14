using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
    }
}
