using Client.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
	public interface INotificationsClientDbContext
	{
		DbSet<ClientModel> NotificationsСlient { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}