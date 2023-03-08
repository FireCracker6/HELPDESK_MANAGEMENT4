using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject currentViewModel;

        [RelayCommand]
        private void GoToAddTicket() => CurrentViewModel = new AddTicketsViewModel();


        [RelayCommand]
        private void GoToTickets() => CurrentViewModel = new TicketsViewModel();

        public MainViewModel()
        {
            CurrentViewModel = new TicketsViewModel();
        }
    }
}
