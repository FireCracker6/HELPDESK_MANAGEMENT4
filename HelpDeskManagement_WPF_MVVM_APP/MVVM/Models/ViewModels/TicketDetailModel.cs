using CommunityToolkit.Mvvm.ComponentModel;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models.ViewModels
{
    public partial class TicketDetailModel : ObservableRecipient
    {
        private readonly DataContext _context;
        private readonly TicketService _ticketService;
        private List<string> _priorityList;
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

        public async Task SaveTicket(Ticket ticket, DataGrid ticketDataGrid)
        {
            if (ticket == null || ticketDataGrid == null) return;

            var selectedTicket = (Ticket)ticketDataGrid.SelectedItem;

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
      

        [ObservableProperty]
        private string pageTitle2 = "Ticket Details";
    }
}
