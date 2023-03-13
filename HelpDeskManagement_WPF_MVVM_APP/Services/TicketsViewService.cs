using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.MVVM.Models;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class TicketsViewService
{
    #region Fields
    private static readonly TicketService _context;
    private static ObservableCollection<TicketModel> tickets;
    #endregion

    #region Constructor
    public TicketsViewService()
    {
        ReturnList();
        var tickets = new TicketService();
    }
    #endregion

    #region Methods
    public static async Task ReturnList()
    {
        var tickets = new TicketService();
        await tickets.GetAllAsync();
    }

    public static ObservableCollection<TicketModel> Tickets()
    {
        var result = _context?.GetAllAsync();

        var tickets = new ObservableCollection<TicketModel>();

        return tickets;
    }

    static TicketsViewService()
    {
        try
        {
            var getAll = new TicketService();
            var customers = getAll.GetAllAsync();
            Debug.WriteLine(getAll.GetAllAsync());
        }
        catch { tickets = new ObservableCollection<TicketModel>(); }
    }
    #endregion
}
