public class TicketPriorities
{
    public int Id { get; set; } 
    public int TicketId { get; set; }
    public string PriorityName { get; set; } = null!;

    public TicketsEntity Tickets { get; set; } = null!;
}
