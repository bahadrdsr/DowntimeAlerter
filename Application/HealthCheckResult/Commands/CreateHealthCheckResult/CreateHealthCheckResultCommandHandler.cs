using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using System;

namespace Application.HealthCheckResult.Commands.CreateHealthCheckResult
{
    public class CreateHealthCheckResultCommandHandler : IRequestHandler<CreateHealthCheckResultCommand, Unit>
    {
        private readonly DataContext _context;
        public CreateHealthCheckResultCommandHandler(DataContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(CreateHealthCheckResultCommand request, CancellationToken cancellationToken)
        {
            var result = new Domain.Entities.HealthCheckResult
            {
                ExecutionTime = DateTime.UtcNow,
                Message = request.Message,
                StatusCode = request.StatusCode,
                TargetAppId = request.TargetAppId
            };
            await _context.HealthCheckResults.AddAsync(result);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}