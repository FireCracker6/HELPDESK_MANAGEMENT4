using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;

public partial class TicketDetailModel : ObservableRecipient
{
    private readonly DataContext _context;
    private readonly TicketService _ticketService;

    public TicketDetailModel()
    {
        _ticketService = new TicketService();
        _context = new DataContext();
    }

    public TicketDetailModel(Guid userId)
    {
        _ticketService = new TicketService();
        SelectedUserId = userId;
        _ = LoadTicketsAsync(userId);
        _context = new DataContext();
    }

    private async Task LoadTicketsAsync(Guid userId)
    {
        var tickets = await _ticketService.GetAsync(userId);
        Tickets = new ObservableCollection<Ticket>(tickets);
    }

    private Guid _selectedUserId;
    public Guid SelectedUserId
    {
        get => _selectedUserId;
        set => SetProperty(ref _selectedUserId, value);
    }

    private Ticket _selectedTicket;
    internal Ticket SelectedTicket
    {
        get => _selectedTicket;
        set => SetProperty(ref _selectedTicket, value);
    }

    private ObservableCollection<Ticket> _tickets;
    internal ObservableCollection<Ticket> Tickets
    {
        get => _tickets;
        set => SetProperty(ref _tickets, value);
    }

    //public ICommand UpdateTicketCommand => new RelayCommand(async () =>
    //{
    //    if (SelectedTicket == null) return;

    //    await UpdateTicketAsync(SelectedTicket);
    //});

    public ICommand UpdateTicketCommand => new DelegateCommand(UpdateTicket);

    private async void UpdateTicket()
    {
        if (SelectedTicket == null) return;

        await UpdateTicketAsync(SelectedTicket);
    }

    public async Task SaveTicket(Ticket ticket)
    {
        if (ticket == null) return;

        var ticketEntity = await _context.Tickets.FindAsync(ticket.Id);

        if (ticketEntity == null)
        {
            throw new ArgumentException($"Ticket with Id {ticket.Id} does not exist.");
        }

        ticketEntity.Title = ticket.Title;
        ticketEntity.Description = ticket.Description;
        ticketEntity.TicketCategory = "General";
        ticketEntity.CreatedAt = ticket.CreatedAt;
        ticketEntity.LastUpdatedAt =DateTime.Now;
        ticketEntity.ClosedAt = ticket.ClosedAt;
        ticketEntity.Priorities = ticket.Priorities.Select(p => new TicketPriorities
        {
            Id = p.Id,
            PriorityName = p.PriorityName
        }).ToList();
        ticketEntity.Statuses = ticket.Statuses.Select(s => new TicketStatuses
        {
            Id = s.Id,
            TicketId = s.TicketId,
            StatusName = s.StatusName
        }).ToList();
        // Update the Comments collection
        ticketEntity.Comments.Clear(); // remove all existing comments
        foreach (var comment in ticket.Comments)
        {
            ticketEntity.Comments.Add(new TicketComments
            {
                Id = comment.Id,
                CommentsText = comment.CommentsText,
                CreatedAt = comment.CreatedAt
            });
        }
        await _context.SaveChangesAsync();
    }


    private async Task UpdateTicketAsync(Ticket ticket)
    {
        var ticketEntity = await _context.Tickets.FindAsync(ticket.Id);

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
        ticketEntity.Priorities = ticket.Priorities;
        ticketEntity.Statuses = ticket.Statuses;
        ticketEntity.Comments= ticket.Comments;


        Debug.WriteLine(ticketEntity.Comments.Count());
        await _context.SaveChangesAsync();
    }



    [ObservableProperty]
    private string pageTitle2 = "Ticket Details";


    // [ObservableProperty]
    //private ObservableCollection<TicketModel> tickets = TicketsViewService.Tickets();


}
