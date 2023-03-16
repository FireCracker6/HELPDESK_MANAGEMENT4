using System.Collections.Generic;

public class TicketStatuses
{
    public int Id { get; set; } 
    public int TicketId { get; set; }
    public string StatusName { get; set; } = null!;
    public TicketsEntity Tickets { get; set; } = null!;
}