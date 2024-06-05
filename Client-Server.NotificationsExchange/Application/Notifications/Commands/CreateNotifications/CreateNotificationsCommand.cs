using System;
using MediatR;

namespace WebAPI.Application.Notifications.Commands.CreateNotifications
{
    public class CreateNotificationsCommand : IRequest
    {
        public string Content { get; set; }
		public double Value { get; set; }
		public char Quality { get; set; }
	}
}
