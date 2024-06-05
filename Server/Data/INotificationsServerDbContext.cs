using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;

namespace Server.Data
{
	public interface INotificationsServerDbContext
	{
		DbSet<ServerModel> NotificationsServer { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
