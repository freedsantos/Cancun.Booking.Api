using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cancun.Booking.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        
        [HttpGet("GetAll")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _userService.Get(id);
        }
    }
}