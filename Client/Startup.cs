using Client.Mappings;
using Client.Data;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Server;
using System.Reflection;

namespace Client
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSignalR();
			services.AddAutoMapper(config =>
			{
				config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
			});

			services.AddEntityExchange(Configuration);
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<NotificationsHub>("/notificationhub");
			});
		}
	}
}

