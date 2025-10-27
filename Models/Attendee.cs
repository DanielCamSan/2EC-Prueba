using System.Collections.Generic;
namespace TecWebFest.Api.Entities
{
    public class Attendee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;

        // 1:1 optional
        public AttendeeProfile? Profile { get; set; }

        // 1:N Attendee -> Tickets
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
