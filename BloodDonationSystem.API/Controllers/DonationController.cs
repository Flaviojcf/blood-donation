using BloodDonationSystem.Application.Commands.CreateDonation;
using BloodDonationSystem.Application.Queries.GetDonationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var getDonationByIdQuery = new GetDonationByIdQuery(id);
            var donation = await _mediator.Send(getDonationByIdQuery);

            return Ok(donation);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll(string query)
        //{
        //    var getAllDonationQuery = new GetAllDonationQuery(query);
        //    var donation = await _mediator.Send(getAllDonationQuery);
        //    return Ok(donation);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonationCommand createDonationCommand)
        {
            var id = await _mediator.Send(createDonationCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createDonationCommand);
        }
    }
}
