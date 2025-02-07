using BloodDonationSystem.Application.Commands.CreateDonor;
using BloodDonationSystem.Application.Queries.GetDonorById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var getDonorByIdQuery = new GetDonorByIdQuery(id);
            var donor = await _mediator.Send(getDonorByIdQuery);

            return Ok(donor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonorCommand createDonorCommand)
        {
            var id = await _mediator.Send(createDonorCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createDonorCommand);
        }
    }
}
