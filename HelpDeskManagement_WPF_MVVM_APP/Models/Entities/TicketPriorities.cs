using System.Collections.Generic;

public class TicketPriorities
{
    public int Id { get; set; } 
    public int TicketId { get; set; }
    public string PriorityName { get; set; } = null!;
   
    public TicketsEntity Tickets { get; set; } = null!;
    public static List<string> PriorityList { get; } = new List<string> { "High", "Medium", "Low" };

}
