using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cancun.Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _reservationService;

        public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpGet("ViewAll")]
        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _reservationService.GetAll();
        }

        [HttpPost("Place")]
        public async Task<Reservation> Create(ReservationCreateDto reservationDto)
        {
            return await _reservationService.Create(reservationDto);
        }

        [HttpPut("Modify")]
        public async Task<Reservation> Update(ReservationUpdateDto reservation)
        {
            return await _reservationService.Update(reservation);
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<bool> Delete(int id)
        {
            await _reservationService.Delete(id);
            return true;
        }
    }
}