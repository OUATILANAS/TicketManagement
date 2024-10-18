using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Ticket> Tickets {get; set;}
    }
}