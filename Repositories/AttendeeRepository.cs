using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;

namespace TecWebFest.Api.Repositories
{
    public class AttendeeRepository : GenericRepository<Attendee>, IAttendeeRepository
    {
        private readonly DbSet<Ticket> _tickets;
        private readonly DbSet<AttendeeProfile> _profiles;

        public AttendeeRepository(AppDbContext ctx) : base(ctx)
        {
            _tickets = ctx.Set<Ticket>();
            _profiles = ctx.Set<AttendeeProfile>();
        }

        public Task<IReadOnlyList<Ticket>> GetTicketsAsync(int attendeeId) =>
            _tickets.Include(t => t.Festival)
                    .Where(t => t.AttendeeId == attendeeId)
                    .ToListAsync();
    }
}
