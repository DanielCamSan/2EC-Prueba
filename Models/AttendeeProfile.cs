namespace TecWebFest.Api.Entities
{
    public class AttendeeProfile
    {
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = default!;

        public string DocumentId { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }
}
