using MediatR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Configuration;
using Server.Data;
using Server.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using Newtonsoft.Json;

namespace Server
{
	public class NotificationsHub : Hub
	{
		public IConfiguration Configuration { get; }
		private readonly ServerDbContext _dbContext;
		public List<ServerModel> OldDataNotifications { get; set; }
		private static List<object> Connections = new();

		public NotificationsHub(ServerDbContext dbContext, IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
			  .AddJsonFile("Configuration/configure.json", optional: false, reloadOnChange: true);
			configuration = builder.Build();
			Configuration = configuration;
			_dbContext = dbContext;
			
		}
		public async Task Enter(Guid clientid, string groupName)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			await Clients.All.SendAsync("Notify", $"{clientid} connected to groups {groupName}");
		}
		public async Task Send(Guid clientid, string groupName)
		{
			var hubConfig = Configuration.GetSection("NotificationsHubSettings").Get<NotificationsHubSettings>();
			
			while (Connections.Count != 0)
			{
				List<ServerModel> NewDataNotifications = await _dbContext.NotificationsServer.ToListAsync();
				if (File.Exists($"SentData/notificationsOldData_{clientid}.json"))
				{
					string jsonOldData = File.ReadAllText($"SentData/notificationsOldData_{clientid}.json");
					OldDataNotifications = JsonConvert.DeserializeObject<List<ServerModel>>(jsonOldData);
				}
				else
				{
					OldDataNotifications = new();
					WriteDataToJsonFile(OldDataNotifications, clientid);
				}
				
			if (hubConfig.UseCompareLists) { NewDataNotifications = await CompareLists(NewDataNotifications, 
				OldDataNotifications, clientid); }

				if (NewDataNotifications.Any())
				{
					foreach (ServerModel data in NewDataNotifications)
					{
						if (hubConfig.TargetClients == "All")
						{
							await Clients.All.SendAsync(hubConfig.HubMethod, data, hubConfig.ServerId);
						}
						else if (hubConfig.TargetClients == "Caller")
						{
							await Clients.Group(groupName).SendAsync(hubConfig.HubMethod, data, hubConfig.ServerId);
						}
					}	
				}
				await Task.Delay(hubConfig.DelayMilliseconds);
			}
		}
		private async Task<List<ServerModel>> CompareLists(List<ServerModel> getData, List<ServerModel> oldData, Guid clientid)
		{
			var newData = new List<ServerModel>();
			foreach (var obj in getData)
			{
				if (!oldData.Exists(x => x.Id == obj.Id)) 
				{
					newData.Add(obj); } 					
			}
			if (newData.Any()) { foreach(var obj in newData) { oldData.Add(obj); }
				await WriteDataToJsonFile(oldData, clientid); }
				
			return  newData;
		}
		private async Task WriteDataToJsonFile(List<ServerModel> Data, Guid clientid)
		{
			string jsonOldData = JsonConvert.SerializeObject(Data, Formatting.Indented);
			File.WriteAllText($"SentData/notificationsOldData_{clientid}.json", jsonOldData);
		}

		public override async Task OnConnectedAsync()
		{
			Connections.Add(Context.ConnectionId);
			Console.WriteLine($"New connection: {Context.ConnectionId}");
			await this.Clients.Others.SendAsync("Notify", $"{Context.ConnectionId} is connected.");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			Connections.Remove(Context.ConnectionId);
			Console.WriteLine($"Disconnecting: {Context.ConnectionId}");
			await this.Clients.Others.SendAsync("Notify", $"{Context.ConnectionId} is disconnected.");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
