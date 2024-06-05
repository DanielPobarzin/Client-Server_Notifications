using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain;

namespace WebAPI.Persistance
{
	public class NotificationConfig : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.HasKey(note => note.id);
			builder.HasIndex(note => note.id).IsUnique();
			builder.Property(note => note.content).HasMaxLength(250);

		}
	}
}
