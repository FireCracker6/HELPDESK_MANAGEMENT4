using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.Views;
using HelpDeskManagement_WPF_MVVM_APP.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;

public partial class TicketDetailModel : ObservableRecipient
{
    #region Fields

    private readonly DataContext _context;
    private readonly TicketService _ticketService;

    private List<string> _priorityList;

    private DataGrid _ticketDataGrid;

    #endregion

    #region Properties

    public DataGrid TicketDataGrid
    {
        get { return _ticketDataGrid; }
        set
        {
            _ticketDataGrid = value;
            OnPropertyChanged(nameof(TicketDataGrid));
        }
    }

    public List<string> PriorityList
    {
        get { return _priorityList; }
        set
        {
            _priorityList = value;
            OnPropertyChanged(nameof(PriorityList));
        }
    }

    private List<string> _statusesList;
    public List<string> StatusesList
    {
        get { return _statusesList; }
        set
        {
            _statusesList = value;
            OnPropertyChanged(nameof(StatusesList));
        }
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

    public ObservableCollection<Ticket> Tickets
    {
        get { return _tickets; }
        set
        {
            _tickets = value;
            OnPropertyChanged(nameof(Tickets));
        }
    }

    #endregion

    #region Constructors

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


        PriorityList = new List<string> { "High", "Medium", "Low" };
        StatusesList = new List<string> { "Opened", "Updated", "Closed" };
    }

    #endregion

    #region Public Methods

    public async Task LoadTicketsAsync(Guid userId)
    {
        var tickets = await _ticketService.GetAsync(userId);
        Tickets = new ObservableCollection<Ticket>(tickets);
        Debug.WriteLine(tickets.Count());
    }

    public async Task SaveTicket(Ticket ticket, DataGrid ticketDataGrid)
    {
        Debug.WriteLine("I am clicked!");
        if (ticket == null || ticketDataGrid == null) return;

        var selectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        Debug.WriteLine($"selected ticket {selectedTicket.Id}");
        var ticketEntity = await _context.Tickets.FindAsync(selectedTicket.Id);

        if (ticketEntity == null)
        {
            throw new ArgumentException($"Ticket with Id {selectedTicket.Id} does not exist.");
        }

        ticketEntity.Title = ticket.Title;
        ticketEntity.Description = ticket.Description;
        ticketEntity.TicketCategory = "General";
        ticketEntity.CreatedAt = ticket.CreatedAt;
        ticketEntity.LastUpdatedAt = DateTime.Now;

        // Check if the status is "Closed" and set ClosedAt to DateTime.Now if it is
        if (ticket.Statuses.Any(s => s.StatusName == "Closed"))
        {
            ticketEntity.ClosedAt = DateTime.Now;
        }
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
        Debug.WriteLine(ticket.Comments);
        await _context.SaveChangesAsync();
        // get the instance of TicketsView
        var mainWindow = Application.Current.MainWindow;
        var ticketsView = FindVisualChild<TicketsView>(mainWindow);

        // update the tickets in the view
        await ticketsView.UpdateTickets();
    }

    private ICommand _saveTicketCommand;
    public ICommand SaveTicketCommand
    {
        get
        {
            if (_saveTicketCommand == null)
            {
                _saveTicketCommand = new RelayCommand(async () => await SaveTicket(SelectedTicket, _ticketDataGrid));
            }
            return _saveTicketCommand;
        }
    }
    public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            if (child != null && child is T)
                return (T)child;
            else
            {
                var childOfChild = FindVisualChild<T>(child!);
                if (childOfChild != null)
                    return childOfChild;
            }
        }
        return null!;
    }
    #endregion
}