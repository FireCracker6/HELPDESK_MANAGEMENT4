using System;
using System.Collections.Generic;
using HelpDeskManagement_WPF_MVVM_APP.Models;
using Microsoft.EntityFrameworkCore;

public class UsersEntity
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public virtual ICollection<TicketsEntity> Tickets { get; set; } = new HashSet<TicketsEntity>();

    public DbContext Context { get; set; } 

    public static implicit operator UsersEntity(Ticket ticket)
    {

        return new UsersEntity
        {
            Id = ticket.UserId,
            Email = ticket.Email,
            FirstName= ticket.FirstName,
            LastName= ticket.LastName,
            
            
        };
    }

    public static implicit operator Ticket(UsersEntity usersEntity)
    {

       // TicketsEntity ticketEntity = null!;
        return new Ticket
        {
          
            Email = usersEntity.Email,
            FirstName = usersEntity.FirstName,
            LastName= usersEntity.LastName,
          
        
      
        };
       
    }


}
