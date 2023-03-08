
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class TicketService
{
    private readonly DataContext _context;

    public TicketService()
    {
        _context= new DataContext();
    }
    public async Task CreateAsync(Ticket ticket, string email)
    {
        UsersEntity usersEntity = ticket;
        usersEntity.Email= email;

        _context.Add(usersEntity);
        await _context.SaveChangesAsync();
    }

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
    //public async Task UpdateTicketAsync(Ticket ticket)
    //{
    //    var ticketEntity = await _context.Tickets
    //        .Include(t => t.Priorities)
    //        .Include(t => t.Statuses)
    //        .FirstOrDefaultAsync(t => t.Id == ticket.Id);

    //    if (ticketEntity == null)
    //    {
    //        throw new ArgumentException($"Ticket with Id {ticket.Id} does not exist.");
    //    }

    //    ticketEntity.Title = ticket.Title;
    //    ticketEntity.Description = ticket.Description;
    //    ticketEntity.TicketCategory = ticket.TicketCategory;
    //    ticketEntity.CreatedAt = ticket.CreatedAt;
    //    ticketEntity.LastUpdatedAt = ticket.LastUpdatedAt;
    //    ticketEntity.ClosedAt = ticket.ClosedAt;

    //    // Update priorities
    //    foreach (var priority in ticket.Priorities)
    //    {
    //        if (priority.Id == 0)
    //        {
    //            // New priority, add to collection
    //            ticketEntity.Priorities.Add(new TicketPriority
    //            {
    //                PriorityName = priority.PriorityName
    //            });
    //        }
    //        else
    //        {
    //            // Existing priority, update its properties
    //            var priorityEntity = ticketEntity.Priorities.FirstOrDefault(p => p.Id == priority.Id);
    //            if (priorityEntity != null)
    //            {
    //                priorityEntity.PriorityName = priority.PriorityName;
    //            }
    //        }
    //    }

    //    // Remove deleted priorities
    //    var prioritiesToRemove = ticketEntity.Priorities.Where(p => !ticket.Priorities.Any(p2 => p2.Id == p.Id)).ToList();
    //    foreach (var priority in prioritiesToRemove)
    //    {
    //        ticketEntity.Priorities.Remove(priority);
    //    }

    //    // Update statuses
    //    foreach (var status in ticket.Statuses)
    //    {
    //        if (status.Id == 0)
    //        {
    //            // New status, add to collection
    //            ticketEntity.Statuses.Add(new TicketStatus
    //            {
    //                StatusName = status.StatusName
    //            });
    //        }
    //        else
    //        {
    //            // Existing status, update its properties
    //            var statusEntity = ticketEntity.Statuses.FirstOrDefault(s => s.Id == status.Id);
    //            if (statusEntity != null)
    //            {
    //                statusEntity.StatusName = status.StatusName;
    //            }
    //        }
    //    }

    //    // Remove deleted statuses
    //    var statusesToRemove = ticketEntity.Statuses.Where(s => !ticket.Statuses.Any(s2 => s2.Id == s.Id)).ToList();
    //    foreach (var status in statusesToRemove)
    //    {
    //        ticketEntity.Statuses.Remove(status);
    //    }

    //    await _context.SaveChangesAsync();
    //}








    //public async Task<IEnumerable<TicketsEntity>> GetAsync(Guid userId)
    //{
    //    return await _context.Tickets.Where(t => t.UsersId == userId).ToListAsync();

    //}
    //public async Task<IEnumerable<TicketsEntity>> GetAsync(Guid userId)
    //{
    //    //var tickets = await _context.Tickets
    //    //    .Include(t => t.Users)
    //    //    .Where(t => t.UsersId == userId)
    //    //    .ToListAsync();

    //    //return tickets.Select(t => (Ticket)t);

    //      return await _context.Tickets.Where(t => t.UsersId == userId).ToListAsync();
    //}










}
