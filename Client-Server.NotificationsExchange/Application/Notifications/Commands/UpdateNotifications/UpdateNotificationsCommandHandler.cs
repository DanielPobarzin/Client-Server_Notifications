using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Interfaces;
using WebAPI.Domain;
using WebAPI.Application.Common.Exceptions;
namespace WebAPI.Application.Notifications.Commands.UpdateNotifications
{
    public class UpdateNotificationsCommandHandler : IRequestHandler<UpdateNotificationsCommand>
    {
        private readonly INotificationsDbContext _dbContext;

        public UpdateNotificationsCommandHandler(INotificationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateNotificationsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notifications.FirstOrDefaultAsync(note => note.id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Notification), request.Id);
            }
            entity.content = request.Content;
            entity.quality = request.Quality;
			entity.value = request.Value;
			await _dbContext.SaveChangesAsync(cancellationToken);

            return;
        }
    }
}


