using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace api.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync2(int page, int pageSize);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<int> GetTotalTicketsCountAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
    }
}
