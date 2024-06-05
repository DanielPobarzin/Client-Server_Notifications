using MediatR;
using System;

namespace WebAPI.Application.Notifications.Commands.UpdateNotifications
{
    public class UpdateNotificationsCommand : IRequest
    {
		public Guid Id { get; set; }
		public string Content { get; set; }
		public double Value { get; set; }
		public char Quality { get; set; }
	}
}
