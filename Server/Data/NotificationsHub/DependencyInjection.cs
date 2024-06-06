using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data.NotificationsHub
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEntityExchangeNotifications(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnectionNotifications"];
            services.AddDbContext<NotificationsDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<INotificationsServerDbContext>(provider => provider.GetService<NotificationsDbContext>());
            return services;
        }
    }
}
