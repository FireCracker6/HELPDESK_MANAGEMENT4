using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;

public partial class AddTicketsViewModel : ObservableObject
{
    [ObservableProperty]
    private string pageTitle = "Add Tickets View";

    private readonly DataContext _context;

   
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TicketCategory { get; set; } = string.Empty;
    public DateTime CreatedAt { get;  set; } = DateTime.Now;
   // public ContactModel SelectedItem { get; set; } = null!;


   
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

        var userEntity = new UsersEntity();

        var ticketUserService = new TickerUserService();

        var ticket = new UsersEntity()
        {


            
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
          
            
          

        };
        var ticketDetails = new TicketsEntity(_context)
        {
            UsersId = ticket.Id,
            Title = Title,
            Description = Description,
            TicketCategory = TicketCategory,

            CreatedAt = CreatedAt,
        };


        UsersEntity usersEntity = ticket;

       userEntity = await userService.CreateAsync(ticket);

        TicketsEntity ticketUserEntity = ticketDetails;
        ticketUserEntity = await ticketService.CreateAsync(ticketDetails);
        ticketUserEntity.UsersId = usersEntity.Id;


    }
   
}
