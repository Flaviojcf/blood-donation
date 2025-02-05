using BloodDonationSystem.Application.Commands.CreateDonation;
using BloodDonationSystem.Application.Queries.GetAddressById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var getAddressByIdQuery = new GetAddressByIdQuery(id);
            var address = await _mediator.Send(getAddressByIdQuery);

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonationCommand createDonationCommand)
        {
            var id = await _mediator.Send(createDonationCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createDonationCommand);
        }
    }
}
