using Client.Models;
using Microsoft.EntityFrameworkCore;


namespace Client.Data
{
    public class ClientDbContext : DbContext, INotificationsClientDbContext
    {
		public DbSet<ClientModel> NotificationsСlient { get; set; }
		public ClientDbContext(DbContextOptions<ClientDbContext> options)
			: base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
        {
			base.OnModelCreating(builder);
			builder.Entity<ClientModel>(entity =>
			{
				entity.ToTable("notificationsclient");
				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnName("id");
				entity.Property(e => e.ClientId).HasColumnName("clientid");
				entity.Property(e => e.Content).HasColumnName("content");
				entity.Property(e => e.CreationDate)
					.ValueGeneratedNever()
					.HasColumnType("timestamp without time zone")
					.HasColumnName("creationdate");
				entity.Property(e => e.Quality)
					.HasMaxLength(1)
					.HasColumnName("quality");
				entity.Property(e => e.ServerId).HasColumnName("serverid");
				entity.Property(e => e.Value).HasColumnName("value");
			});

		}
	}
}