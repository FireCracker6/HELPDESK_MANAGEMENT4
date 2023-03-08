using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;


namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels
{


  
    public partial class TicketsViewModel : ObservableObject
    {
      
        [ObservableProperty]
        private string pageTitle = "Tickets View";


      



        [ObservableProperty]
        private  ObservableCollection<TicketModel> tickets = TicketsViewService.Tickets();



    }
  


}
