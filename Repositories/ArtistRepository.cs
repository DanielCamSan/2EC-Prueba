using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;

namespace TecWebFest.Api.Repositories
{
    public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(AppDbContext ctx) : base(ctx) {}

        public Task<Artist?> GetScheduleAsync(int id) =>
            _db.Include(a => a.Performances)
               .ThenInclude(p => p.Stage)
               .FirstOrDefaultAsync(a => a.Id == id);
    }
}
