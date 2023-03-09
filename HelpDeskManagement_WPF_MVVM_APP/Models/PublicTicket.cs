using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskManagement_WPF_MVVM_APP.Models;

//public class PublicTicket
//{
//    public int Id { get; set; }
//    public Guid UsersId { get; set; }
//    public string Title { get; set; }
//    public string Description { get; set; }
//    public string TicketCategory { get; set; }
//    public DateTime? CreatedAt { get; set; }
//    public DateTime? LastUpdatedAt { get; set; }
//    public DateTime? ClosedAt { get; set; }
//    //public List<TicketPriority> Priorities { get; set; }
//    //public List<TicketStatus> Statuses { get; set; }

//    public PublicTicket(PublicTicket ticket)
//    {
//        Id = ticket.Id;
//        UsersId = ticket.UsersId;
//        Title = ticket.Title;
//        Description = ticket.Description;
//        TicketCategory = ticket.TicketCategory;
//        CreatedAt = ticket.CreatedAt;
//        LastUpdatedAt = ticket.LastUpdatedAt;
//        ClosedAt = ticket.ClosedAt;
//        //    Priorities = ticket.Priorities;
//        //    Statuses = ticket.Statuses;
//        //}
//    }

//}

public class PublicTicket
{
    public int Id { get; set; }
    public Guid UsersId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TicketCategory { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    public PublicTicket()
    {
    }

    public PublicTicket(PublicTicket ticket)
    {
        Id = ticket.Id;
        UsersId = ticket.UsersId;
        Title = ticket.Title;
        Description = ticket.Description;
        TicketCategory = ticket.TicketCategory;
        CreatedAt = ticket.CreatedAt;
        LastUpdatedAt = ticket.LastUpdatedAt;
        ClosedAt = ticket.ClosedAt;
    }
}
