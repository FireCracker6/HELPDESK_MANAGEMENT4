
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using HelpDeskManagement_WPF_MVVM_APP.Models;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class UserService : DataService<UsersEntity>
{
    private readonly DataContext _context;


    public UserService()
    {
        _context = new DataContext();
    }

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
    public override async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            var tickets = _context.Tickets.Where(t => t.UsersId == id);
            foreach (var ticket in tickets)
            {
                _context.Tickets.Remove(ticket);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }





}
