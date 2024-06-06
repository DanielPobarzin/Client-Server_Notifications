using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;

namespace Server.Data.AlarmsHub
{
	public interface IAlarmsServerDbContext
	{
		DbSet<AlarmsModel> AlarmsServer { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
