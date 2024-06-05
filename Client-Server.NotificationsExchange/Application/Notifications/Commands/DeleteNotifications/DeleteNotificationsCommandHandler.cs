using MediatR;
using WebAPI.Application.Interfaces;
using System;
using System.Threading.Tasks;
using WebAPI.Application.Common.Exceptions;
using WebAPI.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Application.Notifications.Commands.DeleteNotifications
{
    public class DeleteNotificationsCommandHandler : IRequestHandler<DeleteNotificationsCommand>
    {
        private readonly INotificationsDbContext _dbContext;
        public DeleteNotificationsCommandHandler(INotificationsDbContext dbContext) => _dbContext = dbContext;
        public async Task Handle(DeleteNotificationsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notifications.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Notification), request.Id);
            }
            _dbContext.Notifications.Remove(entity);   
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
