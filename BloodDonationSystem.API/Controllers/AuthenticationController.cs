using BloodDonationSystem.Application.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var loginRecord = await _mediator.Send(loginCommand);

            return Ok(loginRecord);
        }
    }
}
