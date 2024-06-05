using WebAPI.Domain;
using MediatR;
using WebAPI.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace WebAPI.Application.Notifications.Commands.CreateNotifications
{
    public class CreateNotificationsCommandHandler : IRequestHandler<CreateNotificationsCommand>
    {
        private readonly INotificationsDbContext _dbContext; 
        public CreateNotificationsCommandHandler(INotificationsDbContext dbContext) => 

               _dbContext = dbContext;
        public async Task Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
			{
                content = request.Content,
                quality = request.Quality,
                value = request.Value,
				id = Guid.NewGuid(),
                creationdate = DateTime.Now,

            };
            await _dbContext.Notifications.AddAsync(notification, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
