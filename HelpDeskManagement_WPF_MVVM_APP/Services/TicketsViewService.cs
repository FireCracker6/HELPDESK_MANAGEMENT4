using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskManagement_WPF_MVVM_APP.Services
{

    internal class TicketsViewService
    {
        private Guid userId;
        private static readonly TicketService _context;

       

        public static async Task ReturnList()
        {
            var tickets = new TicketService();
           
           await tickets.GetAllAsync();
        }
        public TicketsViewService()
        {
           
            ReturnList();
            var tickets = new TicketService();
           
           
        }
        private static ObservableCollection<TicketModel> tickets;

        static TicketsViewService()
        {
            try
            {
              
                  var getAll = new TicketService();
                var customers =  getAll.GetAllAsync();
                Debug.WriteLine(getAll.GetAllAsync());

            }
            catch { tickets = new ObservableCollection<TicketModel>(); }
        }

        public static ObservableCollection<TicketModel> Tickets()
        {
        //    var _context = new DataContext(); // Initialize _context

            var result = _context?.GetAllAsync();

            var tickets = new ObservableCollection<TicketModel>(); // Create an empty ObservableCollection

            // TODO: add logic to populate the tickets ObservableCollection

            return tickets;
        }





    }
}
