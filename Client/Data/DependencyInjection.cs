using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Data
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityExchange(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration["DbConnection"];
			services.AddDbContext<ClientDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});
			services.AddScoped<INotificationsClientDbContext>(provider => provider.GetService<ClientDbContext>());
			return services;
		}
	}
}
