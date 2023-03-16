using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Migrations;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;

public partial class AddTicketsViewModel : ObservableObject
{
    [ObservableProperty]
    private string pageTitle = "Add Tickets View";

    private readonly DataContext _context = null!;

    private ComboBoxItem _ticketPriority = null!;
    public ComboBoxItem TicketPriority
    {
        get { return _ticketPriority; }
        set
        {
            _ticketPriority = value;
            PriorityName = _ticketPriority?.Content.ToString() ?? string.Empty;
            OnPropertyChanged(nameof(TicketPriority));
        }
    }

    private ComboBoxItem _ticketStatus = null!;
    public ComboBoxItem TicketStatus
    {
        get { return _ticketStatus; }
        set
        {
            _ticketStatus = value;
            StatusName = _ticketStatus?.Content.ToString() ?? string.Empty;
            OnPropertyChanged(nameof(TicketStatus));
        }
    }

    private ComboBoxItem _ticketCategory = null!;
    public ComboBoxItem TicketCategory
    {
        get { return _ticketCategory; }
        set
        {
            _ticketCategory = value;
            OnPropertyChanged();
        }
    }

    public string? SelectedTicketCategory
    {
        get { return _ticketCategory?.Content.ToString(); }
    }

    public List<ComboBoxItem> TicketCategoryList { get; } = new List<ComboBoxItem>
    {
    new ComboBoxItem { Content = "Network" },
    new ComboBoxItem { Content = "Programming & Software" },
    new ComboBoxItem { Content = "Hardware related" }
    };
    public List<ComboBoxItem> TicketPriorityList { get; } = new List<ComboBoxItem>
    {
    new ComboBoxItem { Content = "High" },
    new ComboBoxItem { Content = "Medium" },
    new ComboBoxItem { Content = "Low" }
    };
    public List<ComboBoxItem> TicketStatusList { get; } = new List<ComboBoxItem>
    {
    new ComboBoxItem { Content = "Not Started" },
    new ComboBoxItem { Content = "Opened" },
    new ComboBoxItem { Content = "In Progress" },
    new ComboBoxItem { Content = "Closed" }
    };


    public AddTicketsViewModel()
    {
       
        TicketCategory = TicketCategoryList[1];
        TicketPriority = TicketPriorityList[1];
        TicketStatus = TicketStatusList[0];


    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string CommentText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string StatusName { get; set; } = string.Empty;
    public string PriorityName { get; set; } = string.Empty;
 



    [ObservableProperty]
    private string firstname = string.Empty;
    [ObservableProperty]
    private string associates = string.Empty;
    [ObservableProperty]
    private string recordlabel = string.Empty;
    [ObservableProperty]
    private string artistsongs = string.Empty;



    [RelayCommand]
    private async Task Add()
    {
        var userService = new UserService();
        var ticketService = new TicketUserService();
        var commentsService = new TicketComments();



        var userEntity = new UsersEntity();

        var ticket = new UsersEntity()
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = string.Format("{0:000-### ## ##}", double.Parse(PhoneNumber)),
        };

        var ticketDetails = new TicketsEntity(_context)
        {
            UsersId = ticket.Id,
            Title = Title,
            Description = Description,
            TicketCategory = SelectedTicketCategory!,
            CreatedAt = CreatedAt,
        };

        // Create a new TicketComments object with the form input data
        var commentDetails = new TicketComments()
        {
            TicketId = ticketDetails.Id,
            CommentsText = CommentText ?? "No comments yet",
            CreatedAt = DateTime.Now,
        };

        // Create a new TicketPriorities object with the form input data
        var priorityDetails = new TicketPriorities()
        {
            TicketId = ticketDetails.Id,
            PriorityName = PriorityName,
        };

        // Create a new Statuses object with the form input data
        var statusDetails = new TicketStatuses()
        {
            TicketId = ticketDetails.Id,
            StatusName = StatusName,
        };

        UsersEntity usersEntity = ticket;

        userEntity = await userService.CreateAsync(ticket);

        TicketsEntity ticketUserEntity = ticketDetails;
        ticketUserEntity = await ticketService.CreateAsync(ticketDetails);
        ticketUserEntity.UsersId = usersEntity.Id;

        // Add the comment, priority, and status to the ticket
        ticketUserEntity.Comments.Add(commentDetails);
        ticketUserEntity.Priorities.Add(priorityDetails);
        ticketUserEntity.Statuses.Add(statusDetails);
        await ticketService.UpdateAsync(ticketUserEntity);
    }




}