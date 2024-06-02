using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Queries.GetNotification
{
    public class GetNotificationsDetailsQueryHandler : IRequestHandler<GetNotificationsDetailsQuery, NotificationDetailsVM>
    {
        private readonly INotificationsDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetNotificationsDetailsQueryHandler(INotificationsDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<NotificationDetailsVM> Handle(GetNotificationsDetailsQuery request, CancellationToken cancellationToken)
        {
            var notificationsQuery = await _dbContext.notifications.FirstOrDefaultAsync(note => note.id == request.id, cancellationToken);
			if (notificationsQuery == null)
            {
                throw new NotFoundException(nameof(Notification), request.id);
            }
            return _mapper.Map<NotificationDetailsVM>(notificationsQuery);
		}
    }
}
