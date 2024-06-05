using MediatR;
using System;

namespace WebAPI.Application.Notifications.Commands.DeleteNotifications
{
    public class DeleteNotificationsCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
