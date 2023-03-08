using System;
using System.Collections.Generic;

namespace HelpDeskManagement_WPF_MVVM_APP.Models;

internal class Ticket
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid UsersId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string TicketCategory { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public User User { get; set; }
    public ICollection<TicketComments> Comments { get; set; } = new List<TicketComments>();

}
internal class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    // add other User properties you need
}
