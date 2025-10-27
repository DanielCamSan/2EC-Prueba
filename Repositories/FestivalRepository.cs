using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;

namespace TecWebFest.Api.Repositories
{
    public class FestivalRepository : GenericRepository<Festival>, IFestivalRepository
    {
        public FestivalRepository(AppDbContext ctx) : base(ctx) {}

        public Task<Festival?> GetWithStagesAsync(int id) =>
            _db.Include(f => f.Stages).FirstOrDefaultAsync(f => f.Id == id);

        public Task<Festival?> GetLineupAsync(int id) =>
            _db
             .Include(f => f.Stages)
                .ThenInclude(s => s.Performances)
                    .ThenInclude(p => p.Artist)
             .FirstOrDefaultAsync(f => f.Id == id);
    }
}
