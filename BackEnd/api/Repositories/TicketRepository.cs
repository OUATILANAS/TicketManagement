using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDBContext _context;

        public TicketRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();  // No pagination, return all tickets
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync2(int page, int pageSize)
        {
            return await _context.Tickets
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalTicketsCountAsync()
        {
            return await _context.Tickets.CountAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task AddTicketAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}