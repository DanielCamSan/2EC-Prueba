namespace TecWebFest.Api.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Category { get; set; } = "General"; // General / VIP

        public int FestivalId { get; set; }
        public Festival Festival { get; set; } = default!;

        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = default!;

        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    }
}
