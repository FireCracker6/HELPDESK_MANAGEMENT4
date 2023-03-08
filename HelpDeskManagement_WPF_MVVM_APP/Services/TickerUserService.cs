
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class TickerUserService
{
    private readonly DataContext _context;

    public TickerUserService()
    {
        _context = new DataContext();
    }

    public async Task CreateAsync(Ticket ticket)
    {
     

        await _context.SaveChangesAsync();
    }

}
