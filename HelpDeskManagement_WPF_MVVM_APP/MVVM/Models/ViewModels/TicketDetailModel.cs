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

    public async Task SaveTicket(PublicTicket ticket)
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
        //ticketEntity.Priorities = ticket.Priorities;
        //ticketEntity.Statuses = ticket.Statuses;

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

        await _context.SaveChangesAsync();
    }



    [ObservableProperty]
    private string pageTitle2 = "Ticket Details";


    // [ObservableProperty]
    //private ObservableCollection<TicketModel> tickets = TicketsViewService.Tickets();


}
