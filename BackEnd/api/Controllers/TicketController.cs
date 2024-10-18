
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Services;
using api.DTOs;

namespace api.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await _ticketService.GetTicketsAsync();  // Fetch all tickets without pagination
            return Ok(tickets);
        }

        [HttpGet("all2")]
        public async Task<ActionResult> GetTickets2(int page = 1, int pageSize = 10)
        {
            var result = await _ticketService.GetTicketsAsync2(page, pageSize);

            return Ok(new
            {
                items = result.Tickets,
                totalCount = result.TotalCount // Return total count for pagination
            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult> PostTicket([FromBody] TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if the model is invalid
            }

            await _ticketService.AddTicketAsync(ticketDto);
            return CreatedAtAction(nameof(GetTicket), new { id = ticketDto.Ticket_ID }, ticketDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, [FromBody] TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if the model is invalid
            }

            await _ticketService.UpdateTicketAsync(id, ticketDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return NoContent();
        }
    }
}