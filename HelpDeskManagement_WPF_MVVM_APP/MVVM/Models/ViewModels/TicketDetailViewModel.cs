using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels
{
    public partial class TicketDetailViewModel : ObservableRecipient
    {

        private readonly TicketService _ticketService;

        public TicketDetailViewModel(Guid userId)
        {
            _ticketService = new TicketService();
            SelectedUserId = userId;
            _ = LoadTicketsAsync(userId);


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

        public ICommand UpdateTicketCommand => new RelayCommand(async () =>
        {
            if (SelectedTicket == null) return;

            await UpdateTicketAsync(SelectedTicket);
        });

        private async Task UpdateTicketAsync(Ticket ticket)
        {
            var ticketEntity = await _ticketService.GetTicketAsync(ticket.Id);

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

          //  await _ticketService.UpdateAsync(ticketEntity);
        }
        public Guid SelectedId { get; set; }


        [ObservableProperty]
        private string pageTitle2 = "Ticket Details";


       // [ObservableProperty]
        //private ObservableCollection<TicketModel> tickets = TicketsViewService.Tickets();


    }





}
