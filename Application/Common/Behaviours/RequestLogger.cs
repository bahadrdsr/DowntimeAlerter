using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IUserAccessor _currentUserService;
        public RequestLogger(ILogger<TRequest> logger, IUserAccessor currentUserService)
        {
            this._currentUserService = currentUserService;
            this._logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("LProject Request: {Name} {@UserId} {@Request}",
                name, _currentUserService.UserId, request);

            return Task.CompletedTask;
        }
    }
}