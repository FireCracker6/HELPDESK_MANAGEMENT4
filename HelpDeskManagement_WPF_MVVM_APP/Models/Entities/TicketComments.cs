using System;

internal class TicketComments
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int TicketsId { get; set; }
    public string CommentsText { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    
    public TicketsEntity Tickets { get; set; } = null!;
}
