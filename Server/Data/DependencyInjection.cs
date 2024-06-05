using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityExchange(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["DbConnection"];
			services.AddDbContext<ServerDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});
			services.AddScoped<INotificationsServerDbContext>(provider => provider.GetService<ServerDbContext>());
			return services;
		}
	}
}
