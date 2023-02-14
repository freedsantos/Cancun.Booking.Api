using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Domain.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> Create(ReservationCreateDto reservationDto);
        Task<Reservation> Update(ReservationUpdateDto reservationDto);
        Task Delete(int id);
    }
}
