using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IAttendeeRepository
    {
        Task AddAsync(Attendee attendee);
        Task<IReadOnlyList<Ticket>> GetTicketsAsync(int attendeeId);
        Task<int> SaveChangesAsync();
    }
}
