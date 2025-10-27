using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;

namespace TecWebFest.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _ctx;
        public TicketRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Ticket ticket)
        {
            await _ctx.Tickets.AddAsync(ticket);
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
