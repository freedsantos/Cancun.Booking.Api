using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cancun.Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomService _roomService;
        private readonly IAvailabilityService _availabilityService;

        public RoomController(ILogger<RoomController> logger, IRoomService roomService, IAvailabilityService availabilityService)
        {
            _logger = logger;
            _roomService = roomService;
            _availabilityService = availabilityService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _roomService.GetAll();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public async Task<Room> Get(int id)
        {
            return await _roomService.Get(id);
        }

        [HttpGet("CheckAvailability/{id}")]
        public async Task<IEnumerable<AvailabilityDto>> GetAvailability(int id)
        {
            return await _availabilityService.GetAvailabilityAsync(id);
        }
    }
}