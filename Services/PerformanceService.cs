using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;

namespace TecWebFest.Api.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IPerformanceRepository _performances;
        private readonly IStageRepository _stages;
        private readonly IArtistRepository _artists;

        public PerformanceService(
            IPerformanceRepository performances,
            IStageRepository stages,
            IArtistRepository artists)
        {
            _performances = performances;
            _stages = stages;
            _artists = artists;
        }

        public async Task AddPerformanceAsync(CreatePerformanceDto dto)
        {
            // Validaciones mínimas (puedes dejarlas opcionales para el examen)
            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("EndTime must be greater than StartTime.");

            if (!await _artists.ExistsAsync(dto.ArtistId))
                throw new ArgumentException("Artist not found.");

            if (!await _stages.ExistsAsync(dto.StageId))
                throw new ArgumentException("Stage not found.");

            // BONUS: evitar solapamiento en la misma Stage
            var overlaps = await _performances.HasOverlapAsync(dto.StageId, dto.StartTime, dto.EndTime);
            if (overlaps)
                throw new InvalidOperationException("The stage already has a performance in this time range.");

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
