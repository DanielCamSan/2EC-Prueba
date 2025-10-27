using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IAttendeeRepository : IGenericRepository<Attendee>
    {
        Task<IReadOnlyList<Ticket>> GetTicketsAsync(int attendeeId);
    }
}
