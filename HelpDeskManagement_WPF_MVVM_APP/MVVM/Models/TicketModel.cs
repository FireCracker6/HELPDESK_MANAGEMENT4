using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskManagement_WPF_MVVM_APP.MVVM.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public Guid UsersId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string TicketCategory { get; set; } = string.Empty;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now;
        public DateTime? ClosedAt { get; set; } = null;

        public string DisplayName => $"{FirstName} - {LastName}";
    }

}
