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
           
           await tickets.GetAll();
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
                var customers =  getAll.GetAll();
                

            }
            catch { tickets = new ObservableCollection<TicketModel>(); }
        }


        public static   ObservableCollection<TicketModel> Tickets()
        {
           var result  =  _context?.GetAll();

            List list = new List();
        
            var returnList = ReturnList();
            Debug.WriteLine($"FUCK! {result}");
       
            return tickets;
         // UsersEntity usersEntity = new UsersEntity();
         
            

        }
      




    }
}
