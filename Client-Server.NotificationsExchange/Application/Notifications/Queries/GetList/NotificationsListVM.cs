using System;
using System.Collections.Generic;

namespace WebAPI.Application.Notifications.Queries.GetList
{
    public class NotificationsListVM
    {
        public IList<NotificationsLookupDTO> Notifications { get; set; }
    }
}
