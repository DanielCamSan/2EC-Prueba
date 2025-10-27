using TecWebFest.Api.Entities;

namespace TecWebFest.Repositories
{
    public interface IAttendeeProfileRepository
    {
        Task AddAsync(AttendeeProfile profile);
        Task<int> SaveChangesAsync();
    }
}
