using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.AlarmsHub
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityExchangeAlarms(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["DbConnectionAlarms"];
			services.AddDbContext<AlarmsDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});
			services.AddScoped<IAlarmsServerDbContext>(provider => provider.GetService<AlarmsDbContext>());
			return services;
		}
	}
}
