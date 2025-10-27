using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IArtistRepository : IGenericRepository<Artist>
    {
        Task<Artist?> GetScheduleAsync(int id);
    }
}
