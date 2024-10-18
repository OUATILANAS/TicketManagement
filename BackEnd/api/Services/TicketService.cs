using api.DTOs;
using api.Models;
using api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace api.Services
{

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<(IEnumerable<TicketDto> Tickets, int TotalCount)> GetTicketsAsync2(int page, int pageSize)
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync2(page, pageSize);
            var totalCount = await _ticketRepository.GetTotalTicketsCountAsync(); // Get total number of tickets

            var ticketDtos = tickets.Select(t => new TicketDto
            {
                Ticket_ID = t.Ticket_ID,
                Description = t.Description,
                Status = t.Status,
                Date = t.Date
            }).ToList();

            return (ticketDtos, totalCount); // Return both tickets and total count
        }


        public async Task<IEnumerable<TicketDto>> GetTicketsAsync()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();  // Fetch all tickets without pagination
            return tickets.Select(t => new TicketDto
            {
                Ticket_ID = t.Ticket_ID,
                Description = t.Description,
                Status = t.Status,
                Date = t.Date
            }).ToList();
        }


        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null) return null;

            return new TicketDto
            {
                Ticket_ID = ticket.Ticket_ID,
                Description = ticket.Description,
                Status = ticket.Status,
                Date = ticket.Date
            };
        }

        public async Task AddTicketAsync(TicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                Description = ticketDto.Description,
                Status = ticketDto.Status,
                Date = ticketDto.Date
            };
            await _ticketRepository.AddTicketAsync(ticket);
        }

        public async Task UpdateTicketAsync(int id, TicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                Ticket_ID = id,
                Description = ticketDto.Description,
                Status = ticketDto.Status,
                Date = ticketDto.Date
            };
            await _ticketRepository.UpdateTicketAsync(ticket);
        }

        public async Task DeleteTicketAsync(int id)
        {
            await _ticketRepository.DeleteTicketAsync(id);
        }

        
    }

}