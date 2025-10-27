using Microsoft.AspNetCore.Mvc;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly IAttendeeService _service;

        public AttendeesController(IAttendeeService service)
        {
            _service = service;
        }

        // POST: api/v1/attendees/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAttendeeDto dto)
        {
            var id = await _service.RegisterAsync(dto);
            return Created($"api/v1/attendees/{id}", new { id });
        }

        // POST: api/v1/attendees/tickets
        [HttpPost("tickets")]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicketDto dto)
        {
            var id = await _service.BuyTicketAsync(dto);
            return Created($"api/v1/attendees/tickets/{id}", new { id });
        }

        // GET: api/v1/attendees/{id}/tickets
        [HttpGet("{id:int}/tickets")]
        public async Task<IActionResult> GetTickets([FromRoute] int id)
        {
            var list = await _service.GetTicketsAsync(id);
            return Ok(list);
        }
    }
}
