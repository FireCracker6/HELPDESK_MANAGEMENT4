

using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class TicketUserService : DataService<TicketsEntity>
{
    //public override async Task<TicketsEntity> CreateAsync(TicketsEntity entity)
    //{
    //    UsersEntity userEntity = null!;
    //    var item = await GetAsync(x => x.UserId == userEntity.Id);
    //    await base.CreateAsync(item);
    //    return item;
    //}

    private readonly DataContext _context;

    public TicketUserService()
    {
        _context = new DataContext();
    }

    public async Task CreateTicketAsync(Ticket ticket)
    {


        await _context.SaveChangesAsync();
    }
    public async Task CreateCommentsAsync(TicketComments comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }


    public override async Task UpdateAsync(TicketsEntity entity)
    {
        var item = await GetAsync(x => x.UsersId == entity.UsersId);
        item.Title = entity.Title;
        item.Description = entity.Description;
        item.TicketCategory = entity.TicketCategory;

        await base.UpdateAsync(item);
    
    }





}
