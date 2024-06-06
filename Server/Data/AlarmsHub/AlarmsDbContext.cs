using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;

namespace Server.Data.AlarmsHub
{
	public class AlarmsDbContext : DbContext, IAlarmsServerDbContext
	{
		public DbSet<AlarmsModel> AlarmsServer { get; set; }
		public AlarmsDbContext(DbContextOptions<AlarmsDbContext> options)
			: base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<AlarmsModel>(entity =>
			{
				entity.ToTable("alarms");
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.Content).HasColumnName("content");
				entity.Property(e => e.CreationDate).HasColumnName("creationdate");
				entity.Property(e => e.Quality).HasColumnName("quality");
				entity.Property(e => e.Value).HasColumnName("value");
			});
		}
	}
}
	