
using HelpDeskManagement_WPF_MVVM_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskManagement_WPF_MVVM_APP.Contexts;

internal class DataContext : DbContext
{
    #region constructors & overrides

    public DataContext()
    {

    }
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Svart\Source\Repos\Webbutveckling\HelpDeskManagement_WPF_MVVM_APP\HelpDeskManagement_WPF_MVVM_APP\Contexts\sql_HelpDesk_DB.mdf;Integrated Security=True;Connect Timeout=30");
    }
    #endregion

    public DbSet<UsersEntity> Users { get; set; } = null!;
   
    public DbSet<TicketsEntity> Tickets { get; set; } = null!;
    public DbSet<TicketComments>  Comments { get; set; } = null!;
    public DbSet<TicketPriorities> Priorities { get; set; } = null!;
    public DbSet<TicketStatuses> Statuses { get; set; } = null!;
    public DbSet<TicketComments> TicketComments { get; set; } = null!;
   
}
