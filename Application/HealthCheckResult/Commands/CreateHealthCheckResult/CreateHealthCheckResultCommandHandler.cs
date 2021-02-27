using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using System;
using System.Net.Http;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace Application.HealthCheckResult.Commands.CreateHealthCheckResult
{
    public class CreateHealthCheckResultCommandHandler : IRequestHandler<CreateHealthCheckResultCommand, Unit>
    {
        private readonly DataContext _context;
        private readonly INotificationSender _notificationSender;
        public CreateHealthCheckResultCommandHandler(DataContext context, INotificationSender notificationSender)
        {
            this._context = context;
            this._notificationSender = notificationSender;
        }

        public async Task<Unit> Handle(CreateHealthCheckResultCommand request, CancellationToken cancellationToken)
        {
            var targetApp = await _context.TargetApps.FindAsync(request.TargetAppId);
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(targetApp.Url))
                {
                    var hcResult = new Domain.Entities.HealthCheckResult
                    {
                        TargetAppId = request.TargetAppId,
                        StatusCode = (int)response.StatusCode,
                        ExecutionTime = DateTime.UtcNow,
                        Result = response.IsSuccessStatusCode ? Domain.Enums.HealthCheckResultType.Healthy : Domain.Enums.HealthCheckResultType.UnHealthy
                    };
                    await _context.HealthCheckResults.AddAsync(hcResult);
                    await _context.SaveChangesAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        var subject = $"{targetApp.Name} is down";
                        var message = $"<p>We couldn't reach {targetApp.Url} at {hcResult.ExecutionTime}</p>";
                        await _notificationSender.SendNotificationEmailAsync(subject, message);
                    }
                }
                return Unit.Value;
            }
        }
    }
}