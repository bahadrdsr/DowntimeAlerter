using Application.Common.Interfaces;
using Infrastructure.Notification;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // services.AddHttpContextAccessor();
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<INotificationSender, NotificationSender>();
            return services;
        }
    }
}