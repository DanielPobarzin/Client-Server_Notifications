using Microsoft.AspNetCore.Http.Connections;
using System;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;
using Microsoft.AspNetCore.SignalR;
using Server.Configuration;


namespace Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
			  .AddJsonFile("Configuration/configure.json", optional: false, reloadOnChange: true);
			configuration = builder.Build();
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));
			services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings:CorsSettings"));
			services.AddSignalR(configure =>
				{
					var serviceSettings = Configuration.GetSection("ServiceSettings").Get<ServiceSettings>();
					configure.KeepAliveInterval = serviceSettings.KeepAliveInterval;
					configure.EnableDetailedErrors = serviceSettings.EnableDetailedErrors;
				});
			services.AddEntityExchange(Configuration);
			services.AddCors(options =>
			{
				var corsSettings = Configuration.GetSection("ServiceSettings:CorsSettings").Get<ServiceSettings>();

				options.AddPolicy(corsSettings.PolicyName, policy =>
				{
					policy.AllowAnyHeader();
					policy.WithMethods(corsSettings.AllowedMethods);
					policy.WithOrigins(corsSettings.AllowedOrigins);
				});
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var transportSettings = Configuration.GetSection("TransportSettings").Get<TransportSettings>();
			var endpointSettings = Configuration.GetSection("TransportSettings:endpoints").Get<TransportSettings>();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors(transportSettings.UseCorsName);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<NotificationsHub>(endpointSettings.Route, options =>
				{
					options.TransportMaxBufferSize = endpointSettings.TransportMaxBufferSize;
					switch (endpointSettings.TypeTransport)
					{
						case "WebSockets":
							options.Transports = HttpTransportType.WebSockets;
							options.WebSockets.CloseTimeout = endpointSettings.CloseTimeout;
							break;
						case "LongPolling":
							options.Transports = HttpTransportType.LongPolling;
							options.LongPolling.PollTimeout = endpointSettings.CloseTimeout; ///?
							break;
						case "ServerSentEvents":
							options.Transports = HttpTransportType.ServerSentEvents;
							break;
						default: break;
					}
				});
			});
		}
	}
}