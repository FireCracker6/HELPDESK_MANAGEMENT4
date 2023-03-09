using System;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using System.Threading.Tasks;

public class TicketComments
{
    internal object UserId;

    public int Id { get; set; }
    public int TicketId { get; set; }
    public int TicketsId { get; set; }
    public string CommentsText { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }

    public TicketsEntity Tickets { get; set; } = null!;

}