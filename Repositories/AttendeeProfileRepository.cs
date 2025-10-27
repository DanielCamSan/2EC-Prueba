using TecWebFest.Api.Data;
using TecWebFest.Api.Entities;

namespace TecWebFest.Repositories
{
    public class AttendeeProfileRepository : IAttendeeProfileRepository
    {
        private readonly AppDbContext _ctx;
        public AttendeeProfileRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(AttendeeProfile profile)
        {
            await _ctx.AttendeeProfiles.AddAsync(profile);
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
