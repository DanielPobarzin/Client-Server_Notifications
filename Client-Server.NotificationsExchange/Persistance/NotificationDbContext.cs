using WebAPI.Application.Interfaces;
using WebAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Persistance
{
	public class NotificationDbContext : DbContext, INotificationsDbContext
	{
		public DbSet<Notification> Notifications { get; set; }
		public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
			: base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new NotificationConfig());
			base.OnModelCreating(builder);
		}
	}
}
