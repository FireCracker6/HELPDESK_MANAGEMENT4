using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HelpDeskManagement_WPF_MVVM_APP.Services;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels;



    public partial class TicketDetailModel : ObservableObject
    {
    public Guid SelectedId { get; set; }

   
    [ObservableProperty]
        private string pageTitle2 = "Ticket Details";


    [ObservableProperty]
        private ObservableCollection<TicketModel> tickets = TicketsViewService.Tickets();


    }




