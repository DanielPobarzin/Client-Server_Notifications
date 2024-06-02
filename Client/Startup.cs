using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Server;

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
				//endpoints.MapControllers();
				//endpoints.MapHub<NotificationHub>("/notificationhub");
			});
		}
	}
}

