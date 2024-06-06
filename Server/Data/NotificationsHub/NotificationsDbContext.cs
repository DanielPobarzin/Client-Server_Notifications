using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;

namespace Server.Data.NotificationsHub
{
    public class NotificationsDbContext : DbContext, INotificationsServerDbContext
    {
        public DbSet<ServerModel> NotificationsServer { get; set; }
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ServerModel>(entity =>
            {
                entity.ToTable("notifications");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Content).HasColumnName("content");
                entity.Property(e => e.CreationDate).HasColumnName("creationdate");
                entity.Property(e => e.Quality).HasColumnName("quality");
                entity.Property(e => e.Value).HasColumnName("value");
            });
        }
    }
}
