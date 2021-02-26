using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using MediatR;
using Persistence;

namespace Application.TargetApp.Commands.DeleteTargetApp
{
    public class DeleteTargetAppCommandHandler : IRequestHandler<DeleteTargetAppCommand, Unit>
    {
        private readonly DataContext _context;
        public DeleteTargetAppCommandHandler(DataContext context)
        {
            this._context = context;
        }
        public async Task<Unit> Handle(DeleteTargetAppCommand request, CancellationToken cancellationToken)
        {
            var targetApp = await _context.TargetApps.FindAsync(request.Id);
            if (targetApp == null) throw new NotFoundException(nameof(Domain.Entities.TargetApp), request.Id);
            _context.TargetApps.Remove(targetApp);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}