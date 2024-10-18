using api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetTicketsAsync();
        Task<(IEnumerable<TicketDto> Tickets, int TotalCount)> GetTicketsAsync2(int page, int pageSize);
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task AddTicketAsync(TicketDto ticketDto);
        Task UpdateTicketAsync(int id, TicketDto ticketDto);
        Task DeleteTicketAsync(int id);
    }
}