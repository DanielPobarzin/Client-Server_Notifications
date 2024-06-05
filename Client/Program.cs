using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Client.Data;
using Client.Models;
using MediatR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Server.Configuration;
using Server.Data;
using Server;
using Server.Models;

namespace Client
{
	public class Program 
	{
		public static HubConnection connection;
		private static readonly Guid ClientId = Guid.Parse("bc3c9faa-70f7-40cd-95d7-bddc629cc3f1");
		static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				connection = new HubConnectionBuilder()
				.WithUrl("https://localhost:7097/notificationhub",options =>
				{
					options.Transports = HttpTransportType.WebSockets;
				})
				.WithAutomaticReconnect()
				.Build();
				

				connection.ServerTimeout = TimeSpan.FromMinutes(2);
				var context = serviceProvider.GetRequiredService<ClientDbContext>();
				
				connection.On<ServerModel, Guid>("ShowNotification", async (ServerModelNotification, ServerId) =>
				{
						var client = new ClientModel()
						{
						ClientId = ClientId,
						ServerId = ServerId,
						Id = ServerModelNotification.Id,
						Content = ServerModelNotification.Content,
						Value = ServerModelNotification.Value,
						Quality = ServerModelNotification.Quality,
						CreationDate = ServerModelNotification.CreationDate
						};
						Console.WriteLine($"The ñlient {ClientId} received a notification {client.Id} from the server {ServerId}.");
					try
					{
						await context.NotificationsÑlient.AddAsync(client);
					}
					catch (Exception ex) { Console.WriteLine($"The exception: {ex}"); }
					finally { await context.SaveChangesAsync(); }
				});

				connection.Reconnecting += (exception) =>
				{
					Console.WriteLine("Connection lost. Reconnecting...");
					return Task.CompletedTask;
				};
				await connection.StartAsync();
				try { await connection.InvokeAsync("Enter", ClientId, "Group"); }
				catch (Exception ex) { Console.WriteLine($"{ex}"); }

				try { await connection.InvokeAsync("Send", ClientId, "Group"); }
				catch (Exception ex) { Console.WriteLine($"{ex}"); }
				host.Run();

			}
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}
