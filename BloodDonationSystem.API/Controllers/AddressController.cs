using BloodDonationSystem.Application.Commands.CreateAddress;
using BloodDonationSystem.Application.Queries.GetAddressById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var address = await _mediator.Send(new GetAddressByIdQuery(id));

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand createAddressCommand)
        {
            var id = await _mediator.Send(createAddressCommand);

            return CreatedAtAction(nameof(GetById), new { id }, createAddressCommand);
        }
    }
}
