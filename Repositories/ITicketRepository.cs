using TecWebFest.Api.Entities;

namespace TecWebFest.Repositories
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<int> SaveChangesAsync();
    }
}
