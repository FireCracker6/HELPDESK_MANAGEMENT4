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
using System.ComponentModel;
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
    public Ticket SelectedTicket
    {
        get { return _selectedTicket; }
        set
        {
            _selectedTicket = value;
            Debug.WriteLine($"SelectedTicket set to {value?.Email}");
            RaisePropertyChanged(nameof(SelectedTicket));
        }
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
    private string _selectedPriority;


    // For a message after updating to the user
    private string _message;
    public string Message
    {
        get { return _message; }
        set { _message = value; OnPropertyChanged(nameof(Message)); }
    }

    private bool _isMessageVisible;
    public bool IsMessageVisible
    {
        get { return _isMessageVisible; }
        set { _isMessageVisible = value; OnPropertyChanged(nameof(IsMessageVisible)); }
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
       
        _context = new DataContext();
        
     
       
        SelectedTicket = new Ticket();
        


        _ = LoadTicketsAsync(userId);
    }
    public void RaisePropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Public Methods

    public async Task LoadTicketsAsync(Guid userId)
    {
      
        var tickets = await _ticketService.GetAsync(userId);
        Tickets = new ObservableCollection<Ticket>(tickets);
        // Debug.WriteLine(tickets.Count());
       
    }
   

    public async Task SaveTicket(Ticket ticket, DataGrid ticketDataGrid)
    {
        if (ticket == null || ticketDataGrid == null) return;

        var selectedTicket = (Ticket)ticketDataGrid.SelectedItem;
        var ticketEntity = await _context.Tickets.FindAsync(selectedTicket.Id);

        if (ticketEntity == null)
        {
            throw new ArgumentException($"Ticket with Id {selectedTicket.Id} does not exist.");
        }

      
        var userEntity = await _context.Users.FindAsync(selectedTicket.UsersId);
        if (userEntity != null)
        {
            userEntity.FirstName = ticket.FirstName;
            userEntity.LastName = ticket.LastName;
            userEntity.Email = ticket.Email;
       //     userEntity.PhoneNumber = ticket.PhoneNumber; // NOT YET IMPLEMENTED
        }
        ticketEntity.Title = ticket.Title;
        ticketEntity.Description = ticket.Description;
        //   ticketEntity.TicketCategory = ticket.TicketCategory;  // NOT YET IMPLEMENTED
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

        // Track changes to the entity
        _context.Entry(ticketEntity).State = EntityState.Modified;

        // Commit changes to the database
        await _context.SaveChangesAsync();

        // update the tickets in the view
        var mainWindow = Application.Current.MainWindow;
        var ticketsView = FindVisualChild<TicketsView>(mainWindow);
        await ticketsView.UpdateTickets();
       
    }
    private async Task DisplayMessage(string message, TimeSpan displayTime)
    {
        Message = message;
        IsMessageVisible = true;
        await Task.Delay(displayTime);
        IsMessageVisible = false;
        Message = string.Empty;
    }


    private ICommand _saveTicketCommand;
    public ICommand SaveTicketCommand
    {
        get
        {
            if (_saveTicketCommand == null)
            {
                _saveTicketCommand = new RelayCommand(async () =>
                {
                    if (SelectedTicket == null || _ticketDataGrid.SelectedItem == null)
                    {
                        await DisplayMessage("No updates made you must edit a column!", TimeSpan.FromSeconds(3));
                        return;
                    }

                    await SaveTicket(SelectedTicket, _ticketDataGrid);
                    await DisplayMessage($"Updates succcessfull for selected ticket id: {SelectedTicket.Id} with the email-address: {SelectedTicket.Email}", TimeSpan.FromSeconds(3));
                });
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