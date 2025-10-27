using TecWebFest.Api.Entities;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IFestivalRepository : IGenericRepository<Festival>
    {
        Task<Festival?> GetWithStagesAsync(int id);
        Task<Festival?> GetLineupAsync(int id);
    }
}
