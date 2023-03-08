
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelpDeskManagement_WPF_MVVM_APP.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskManagement_WPF_MVVM_APP.Services;

internal class DataService<T> where T : class
{
    private readonly DataContext _context;

	public DataService()
	{
		_context= new DataContext();
	}

	public virtual async Task<T> CreateAsync(T entity)
	{
		_context.Set<T>().Add(entity);
		await _context.SaveChangesAsync();	
		return entity;
	}

	public virtual async Task<IEnumerable<T>> GetAllAsync()
	{
		return await _context.Set<T>().ToListAsync();
	}

    //public virtual async Task<T> GetAsync(Func<T, bool> predicate)
    //{
    //	return await _context.Set<T>().FindAsync(predicate) ?? null!;
    //}

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {

			return await _context.Set<T>().FirstOrDefaultAsync(predicate) ?? null!;
    }

    public virtual async Task UpdateAsync( T entity)
    {
			
            _context.Update(entity);
            await _context.SaveChangesAsync();

    }
    public virtual async Task UpdateRecordAsync(T entity, Guid id)
    {
        // Find the existing entity in the database by ID
        T existingEntity = await _context.Set<T>().FindAsync(id) ?? null!;

        // If the entity exists, update its properties with the new values
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }




    //  public virtual async Task DeleteAsync(Func<T, bool> predicate)
    //  {
    //      var item =  await _context.Set<T>().FindAsync(predicate);
    //if (item != null) 
    //{
    //	_context.Remove(item);
    //	await _context.SaveChangesAsync();
    //}

    //  }

    public virtual async Task DeleteAsync(Guid id)
    {
        var item = await _context.Set<T>().FindAsync(id);
        if (item != null)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }




}
