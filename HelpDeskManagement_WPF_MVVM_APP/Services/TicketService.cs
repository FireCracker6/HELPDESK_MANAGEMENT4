
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class TicketService
{
    #region Private Fields

    private readonly DataContext _context;

    #endregion

    #region Constructor

    public TicketService()
    {
        _context = new DataContext();
    }

    #endregion


    #region public methods
    // Creates a new ticket for the user with the given email
    public async Task CreateAsync(Ticket ticket, string email)
    {
        UsersEntity usersEntity = ticket;
        usersEntity.Email = email;

        _context.Add(usersEntity);
        await _context.SaveChangesAsync();
    }
    // Get all tickets
    public async Task<IEnumerable<TicketModel>> GetAllAsync()
    {
        var tickets = await _context.Tickets
            .Include(t => t.Users)
            .Include(t => t.Comments)
            .Select(t => new TicketModel
            {
                Id = t.Id,
                UsersId= t.Users.Id,
                FirstName = t.Users.FirstName,
                LastName = t.Users.LastName,
                Email = t.Users.Email,
                PhoneNumber = t.Users.PhoneNumber,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                TicketCategory = t.TicketCategory,
                LastUpdatedAt = t.LastUpdatedAt,
                ClosedAt = t.ClosedAt
            })
            .ToListAsync();

        return tickets;
    }


public async Task<IEnumerable<Ticket>> GetAsync(Guid userId)
    {
        var tickets = await _context.Tickets
             .Include(t => t.Users)
             .Include(t => t.Comments)
             .Include(t => t.Priorities)
             .Include(t => t.Statuses)
             .Where(t => t.UsersId == userId)
             .Select(t => new Ticket
             {
                 Id = t.Id,
                 UsersId = t.Users.Id,
                 FirstName = t.Users.FirstName,
                 LastName = t.Users.LastName,
                 Email = t.Users.Email,
                 PhoneNumber = t.Users.PhoneNumber,
                 Title = t.Title,
                 Description = t.Description,
                 CreatedAt = t.CreatedAt,
                 Comments = t.Comments.Count > 0 ? t.Comments.Select(c => new TicketComments
                 {
                     Id = c.Id,
                     TicketId = c.TicketId,
                     CommentsText = c.CommentsText,
                     CreatedAt = c.CreatedAt
                 }).ToList() : new List<TicketComments>(),
                 Priorities = t.Priorities.Count > 0 ? t.Priorities.Select(p => new TicketPriorities
                 {
                     Id = p.Id,
                     TicketId = p.TicketId,
                     PriorityName = p.PriorityName
                 }).ToList() : new List<TicketPriorities>(),
                 Statuses = t.Statuses.Count > 0 ? t.Statuses.Select(s => new TicketStatuses
                 {
                     Id = s.Id,
                     TicketId = s.TicketId,
                     StatusName = s.StatusName
                 }).ToList() : new List<TicketStatuses>()
             })
             .ToListAsync();

        Debug.WriteLine(tickets.Count());
        return tickets;

    }
    public async Task<Ticket> GetTicketAsync(int id)
    {
        return await _context.Tickets
            .Include(t => t.Priorities)
            .Include(t => t.Statuses)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);
    }


    public async Task UpdateTicketAsync(Ticket ticket)
    {
        var ticketEntity = await _context.Tickets
            .Include(t => t.Priorities)
            .Include(t => t.Statuses)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == ticket.Id);

        if (ticketEntity == null)
        {
            throw new ArgumentException($"Ticket with Id {ticket.Id} does not exist.");
        }

        ticketEntity.Title = ticket.Title;
        ticketEntity.Description = ticket.Description;
        ticketEntity.TicketCategory = ticket.TicketCategory;
        ticketEntity.CreatedAt = ticket.CreatedAt;
        ticketEntity.LastUpdatedAt = ticket.LastUpdatedAt;
        ticketEntity.ClosedAt = ticket.ClosedAt;

        // Update priorities
        foreach (var priority in ticket.Priorities)
        {
            if (priority.Id == 0)
            {
                ticketEntity.Priorities.Add(new TicketPriorities
                {
                    PriorityName = priority.PriorityName
                });
            }
            else
            {
                var priorityEntity = ticketEntity.Priorities.FirstOrDefault(p => p.Id == priority.Id);
                if (priorityEntity != null)
                {
                    priorityEntity.PriorityName = priority.PriorityName;
                }
            }
        }

        // Update statuses
        foreach (var status in ticket.Statuses)
        {
            if (status.Id == 0)
            {
                ticketEntity.Statuses.Add(new TicketStatuses
                {
                    StatusName = status.StatusName
                });
            }
            else
            {
                var statusEntity = ticketEntity.Statuses.FirstOrDefault(s => s.Id == status.Id);
                if (statusEntity != null)
                {
                    statusEntity.StatusName = status.StatusName;
                }
            }
        }

        // Update comments
        foreach (var comment in ticket.Comments)
        {
            var commentEntity = ticketEntity.Comments.FirstOrDefault(c => c.Id == comment.Id);
            if (commentEntity != null)
            {
                commentEntity.CommentsText = comment.CommentsText;
            }
        }

        await _context.SaveChangesAsync();
    }


}
#endregion