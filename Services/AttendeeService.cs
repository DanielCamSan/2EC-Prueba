using TecWebFest.Api.DTOs;
using TecWebFest.Api.Entities;
using TecWebFest.Api.Repositories.Interfaces;
using TecWebFest.Api.Services.Interfaces;
using TecWebFest.Repositories;

namespace TecWebFest.Api.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _attendees;
        private readonly IAttendeeProfileRepository _profiles;
        private readonly ITicketRepository _tickets;

        public AttendeeService(
            IAttendeeRepository attendees,
            IAttendeeProfileRepository profiles,
            ITicketRepository tickets)
        {
            _attendees = attendees;
            _profiles  = profiles;
            _tickets   = tickets;
        }

        public async Task<int> RegisterAsync(RegisterAttendeeDto dto)
        {
            var a = new Attendee { FullName = dto.FullName, Email = dto.Email };
            await _attendees.AddAsync(a);
            await _attendees.SaveChangesAsync(); // guarda para obtener Id

            if (dto.DocumentId != null && dto.BirthDate != null)
            {
                var p = new AttendeeProfile
                {
                    AttendeeId = a.Id,
                    DocumentId = dto.DocumentId!,
                    BirthDate  = dto.BirthDate!.Value
                };
                await _profiles.AddAsync(p);
                await _profiles.SaveChangesAsync();
            }
            return a.Id;
        }

        public async Task<int> BuyTicketAsync(BuyTicketDto dto)
        {
            var t = new Ticket
            {
                AttendeeId  = dto.AttendeeId,
                FestivalId  = dto.FestivalId,
                Category    = dto.Category,
                Price       = dto.Price,
                PurchasedAt = DateTime.UtcNow
            };
            await _tickets.AddAsync(t);
            await _tickets.SaveChangesAsync();
            return t.Id;
        }

        public async Task<IReadOnlyList<TicketDto>> GetTicketsAsync(int attendeeId)
        {
            var items = await _attendees.GetTicketsAsync(attendeeId);
            return items.Select(t => new TicketDto
            {
                Id          = t.Id,
                Festival    = t.Festival.Name,
                Category    = t.Category,
                Price       = t.Price,
                PurchasedAt = t.PurchasedAt
            }).ToList();
        }
    }
}
