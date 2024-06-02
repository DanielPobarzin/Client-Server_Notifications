using Domain;
using MediatR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server
{
	public class NotificationsHub : Hub
	{
		private readonly NotificationDbContext _dbContext;
		private readonly Guid _serverid = Guid.NewGuid();
		private List<Notification> OldDataNotifications = new List<Notification>();
		public NotificationsHub(NotificationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task Send()
		{
			while (true)
				{
					List<Notification> NewDataNotifications = await _dbContext.notifications.ToListAsync();
					await Task.Yield();
					CompareLists(OldDataNotifications, NewDataNotifications);
					if (NewDataNotifications.Any())
						foreach (Notification data in NewDataNotifications)
						{
							OldDataNotifications.Add(data);
							await Clients.All.SendAsync("ShowNotification", data, _serverid);
						}
					await Task.Delay(1000);
				}
		}

		private static void CompareLists(List<Notification> oldData, List<Notification> newData)
		{
			foreach (var obj in oldData)
			{
				if (newData.Contains(obj))
					newData.Remove(obj);
			}
		}

		public override async Task OnConnectedAsync()
		{
			var context = Context.GetHttpContext();
			Debug.WriteLine($"New connection: {Context.ConnectionId}");
			await this.Clients.All.SendAsync("Notify", $"{Context.ConnectionId} is connected.");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			Debug.WriteLine($"Disconnecting: {Context.ConnectionId}");
			await this.Clients.All.SendAsync("Notify", $"{Context.ConnectionId} is disconnected.");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
