using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;

namespace TecWebFest.Api.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly AppDbContext _ctx;
        public AttendeeRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(Attendee attendee)
        {
            await _ctx.Attendees.AddAsync(attendee);
        }

        public Task<IReadOnlyList<Ticket>> GetTicketsAsync(int attendeeId)
        {
            return _ctx.Tickets
                .Include(t => t.Festival)
                .Where(t => t.AttendeeId == attendeeId)
                .ToListAsync()
                .ContinueWith<IReadOnlyList<Ticket>>(t => t.Result);
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
