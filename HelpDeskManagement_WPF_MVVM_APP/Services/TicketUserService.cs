

using System.Threading.Tasks;

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

    public override async Task UpdateAsync(TicketsEntity entity)
    {
        var item = await GetAsync(x => x.UsersId == entity.UsersId);
        item.Title = entity.Title;
        item.Description = entity.Description;
        item.TicketCategory = entity.TicketCategory;

        await base.UpdateAsync(item);
    
    }


}
