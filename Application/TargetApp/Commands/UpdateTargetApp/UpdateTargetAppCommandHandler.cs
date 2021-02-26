using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TargetApp.Commands.UpdateTargetApp
{
    public class UpdateTargetAppCommandHandler : IRequestHandler<UpdateTargetAppCommand, Unit>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public UpdateTargetAppCommandHandler(DataContext context, IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
            this._context = context;

        }
        public async Task<Unit> Handle(UpdateTargetAppCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.UserId);

            var targetApp = await _context.TargetApps.FindAsync(request.Id);
            targetApp.Name = request.Name;
            targetApp.Url = request.Url;
            targetApp.Interval = request.Interval;
            targetApp.IsActive = request.IsActive;
            targetApp.LastModified = DateTime.UtcNow;

            _context.TargetApps.Update(targetApp);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}