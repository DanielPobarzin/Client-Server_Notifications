using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Client
{
	public class Program
	{
		 private static HubConnection connection;

		public static void Main(string[] args)
		{
			connection = new HubConnectionBuilder()
				.WithUrl("https://localhost:7097/notificationhub")
				.WithAutomaticReconnect()
				.Build();
			connection.On<Notification, Guid>("ShowNotification", (notification, serverid) =>
			{
				
				//Debug.WriteLine($"Notification received: {notification.id}: {notification.content}");
			});
			connection.StartAsync().GetAwaiter().GetResult();

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}
