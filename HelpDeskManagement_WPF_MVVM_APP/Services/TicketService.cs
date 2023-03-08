
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
                }).ToList() : new List<TicketComments>()
            })
            .ToListAsync();
        Debug.WriteLine(tickets.Count());
        return tickets;
    }









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
