using MediatR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Configuration;
using Server.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using Newtonsoft.Json;
using Server.Data.NotificationsHub;

namespace Server.Hubs
{
    public class NotificationsHub : Hub
	{
		private IConfiguration Configuration { get; }
		private readonly NotificationsDbContext _dbContext;
		private List<ServerModel>? OldDataNotifications { get; set; }
		private static Dictionary<string, string> _сonnections = new();

		public NotificationsHub(NotificationsDbContext dbContext, IConfiguration configuration){

			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("Configuration/configure.json", optional: false, reloadOnChange: true);
			configuration = builder.Build();
			Configuration = configuration; ///
			_dbContext = dbContext;
			
		}

		public async Task Enter(Guid clientId, string groupName){

			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			_сonnections[Context.ConnectionId] = "Connected";
			await Clients.Others.SendAsync("Notify", $"Client {clientId} joined the <<{groupName}>> group");
		}

		public async Task Send(Guid clientid)	{

			var hubConfig = Configuration.GetSection("NotificationsHubSettings").Get<NotificationsHubSettings>();

			while (_сonnections.Count != 0)	{

				List<ServerModel> NewDataNotifications = await _dbContext.NotificationsServer.ToListAsync();
				if (File.Exists($"Data/NotificationsHub/SentData/notificationsOldData_{clientid}.json")) {

					string jsonOldData = File.ReadAllText($"Data/NotificationsHub/SentData/notificationsOldData_{clientid}.json");
					OldDataNotifications = JsonConvert.DeserializeObject<List<ServerModel>>(jsonOldData);
				} else {

					OldDataNotifications = new();
					await WriteDataToJsonFile(OldDataNotifications, clientid);
				}
				
				if (hubConfig.UseCompareLists) { NewDataNotifications = await CompareLists(NewDataNotifications, 
				OldDataNotifications, clientid, Context.ConnectionId); }

				if (NewDataNotifications.Count != 0) {

					foreach (ServerModel data in NewDataNotifications)	{

						if (hubConfig.TargetClients == "ContextClient")	{

							await Clients.Client(Context.ConnectionId).SendAsync(hubConfig.HubMethod, data, hubConfig.ServerId);
						} else	{

							await Clients.All.SendAsync(hubConfig.HubMethod, data, hubConfig.ServerId);
						}
					}	
				}
				await Task.Delay(hubConfig.DelayMilliseconds);
			}
		}

		private async Task<List<ServerModel>> CompareLists(List<ServerModel> recievedData, List<ServerModel> sentData, Guid clientId, string connect) {

			var dataWillSent = new List<ServerModel>();

			if (_сonnections.ContainsKey(connect)) {

				foreach (var notification in recievedData) {

					if (!sentData.Exists(x => x.Id == notification.Id)) { 
						
						dataWillSent.Add(notification);
					}
				} 

				if (dataWillSent.Any()) {

					foreach (var notification in dataWillSent) {

						sentData.Add(notification); }
					await WriteDataToJsonFile(sentData, clientId);
				}
			}
			return dataWillSent;
		}
		private async Task WriteDataToJsonFile(List<ServerModel> Data, Guid clientid) {

			string jsonOldData = JsonConvert.SerializeObject(Data, Formatting.Indented);
			await Task.Run(() => File.WriteAllText($"Data/NotificationsHub/SentData/notificationsOldData_{clientid}.json", jsonOldData));
		}

		public override async Task OnConnectedAsync() { 

			Console.WriteLine($"New connection: {Context.ConnectionId}");
			await this.Clients.Others.SendAsync("Notify", $"{Context.ConnectionId} is connected.");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception) {

			_сonnections.Remove(Context.ConnectionId);
			Console.WriteLine($"Disconnecting: {Context.ConnectionId}");
			await this.Clients.Others.SendAsync("Notify", $"{Context.ConnectionId} is disconnected.");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
