using Microsoft.AspNetCore.Http.Connections;
using System;
using Persistance;
using System.Collections.Generic;
using AutoMapper;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Common.Mappings;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace Server
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
			services.AddAutoMapper(config =>
			{
				config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
				config.AddProfile(new AssemblyMappingProfile(typeof(INotificationsDbContext).Assembly));
			});

			services.AddApplication();
			services.AddSignalR();
			services.AddPersistance(Configuration);
			//services.AddControllers();
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			});
			//services.AddSwaggerGen();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();
			//app.UseSwagger();
			//app.UseSwaggerUI();
			app.UseRouting();
			app.UseCors("AllowAll");
			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapHub<NotificationsHub>("/notificationhub", options =>
				{
					options.ApplicationMaxBufferSize = 128;
					options.TransportMaxBufferSize = 128;
					options.LongPolling.PollTimeout = TimeSpan.FromMinutes(1);
					options.Transports = HttpTransportType.WebSockets;
					options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);

				});
			});
		}
	}
}
