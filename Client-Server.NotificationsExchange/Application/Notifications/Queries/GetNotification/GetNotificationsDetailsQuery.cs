using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Notifications.Queries.GetNotification
{
    public class GetNotificationsDetailsQuery : IRequest<NotificationDetailsVM>
    {
        public Guid Id { get; set; }
    }
}
