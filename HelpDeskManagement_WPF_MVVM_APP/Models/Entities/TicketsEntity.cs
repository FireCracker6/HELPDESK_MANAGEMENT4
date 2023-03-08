﻿using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using System.Collections.Generic;
using System;
using System.Linq;

internal class TicketsEntity
{
    private readonly DataContext _context;

    public TicketsEntity(DataContext context)
    {
        _context = context;
    }

    public int Id { get; set; }
    public Guid UsersId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string TicketCategory { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public virtual ICollection<TicketComments> Comments { get; set; } = new HashSet<TicketComments>();
    public virtual UsersEntity Users { get; set; } = null!;
    public static implicit operator Ticket(TicketsEntity entity)
    {
        return new Ticket
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UsersId = entity.UsersId,
            FirstName = entity.Users.FirstName,
            LastName = entity.Users.LastName,
            Email = entity.Users.Email,
            PhoneNumber = entity.Users.PhoneNumber,
            Title = entity.Title,
            Description = entity.Description,
            TicketCategory = entity.TicketCategory,
            CreatedAt = entity.CreatedAt,
            Comments = entity.Comments.Select(c => new TicketComments
            {
                Id = c.Id,
                TicketsId = c.TicketsId, // Use TicketId instead of TicketsId
                CommentsText = c.CommentsText,
                CreatedAt = c.CreatedAt
            }).ToList()
        };
    }

}
