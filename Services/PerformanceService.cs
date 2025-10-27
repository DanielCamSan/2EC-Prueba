using Microsoft.EntityFrameworkCore;
using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IGenericRepository<Performance> _performances;
        private readonly IGenericRepository<Stage> _stages;
        private readonly IGenericRepository<Artist> _artists;

        public PerformanceService(IGenericRepository<Performance> performances,
                                  IGenericRepository<Stage> stages,
                                  IGenericRepository<Artist> artists)
        {
            _performances = performances;
            _stages = stages;
            _artists = artists;
        }

        public async Task AddPerformanceAsync(CreatePerformanceDto dto)
        {
            // TODO: (Exam) validate no overlap on same stage
            var entity = new Performance
            {
                ArtistId = dto.ArtistId,
                StageId = dto.StageId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };
            await _performances.AddAsync(entity);
            await _performances.SaveChangesAsync();
        }
    }
}
