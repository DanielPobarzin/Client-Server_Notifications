using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Application.Interfaces
{
	public interface INotificationsDbContext
	{
		DbSet<Notification> Notifications { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
