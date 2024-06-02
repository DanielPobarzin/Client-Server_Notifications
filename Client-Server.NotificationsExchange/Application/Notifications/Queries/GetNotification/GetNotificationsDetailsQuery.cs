using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Queries.GetNotification
{
    public class GetNotificationsDetailsQuery : IRequest<NotificationDetailsVM>
    {
        public Guid id { get; set; }
    }
}
