using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Data;

namespace Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				try
				{
					var context = serviceProvider.GetRequiredService<ServerDbContext>();
					DbInitializer.Initialize(context);
				}
				catch (Exception exception)
				{
					// log
				}

			}
			host.Run();
		}
		public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}
