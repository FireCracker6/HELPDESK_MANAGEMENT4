
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Models;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class UserService : DataService<UsersEntity>
{
    public override Task<IEnumerable<UsersEntity>> GetAllAsync()
    {
        
        return base.GetAllAsync();
    }

    public override async Task UpdateAsync(UsersEntity entity)
    {
        var item = await GetAsync(x => x.Id == entity.Id);
        if (item != null)
        {
            item.FirstName = entity.FirstName;
            item.LastName = entity.LastName;
            item.Email = entity.Email;

            await base.UpdateAsync(item);
        }
    }

    public override Task<UsersEntity> GetAsync(Expression<Func<UsersEntity, bool>> predicate)
    {
        return base.GetAsync(predicate);
    }

    public override Task UpdateRecordAsync(UsersEntity entity, Guid id)
    {
        return base.UpdateRecordAsync(entity, id);
    }




}
