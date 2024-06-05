using AutoMapper;
using MediatR;
using WebAPI.Application.Interfaces;
using System;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Application.Notifications.Queries.GetList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNotificationsListQuery, NotificationsListVM>
    {
        private readonly INotificationsDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetNoteListQueryHandler(INotificationsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<NotificationsListVM> Handle(GetNotificationsListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _dbContext.Notifications
                .ProjectTo<NotificationsLookupDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NotificationsListVM { Notifications = notesQuery };
        }
    }
}
