using TecWebFest.Api.DTOs;

namespace TecWebFest.Api.Services.Interfaces
{
    public interface IAttendeeService
    {
        Task<int> RegisterAsync(RegisterAttendeeDto dto);
        Task<int> BuyTicketAsync(BuyTicketDto dto);
        Task<IReadOnlyList<TicketDto>> GetTicketsAsync(int attendeeId);
    }
}
