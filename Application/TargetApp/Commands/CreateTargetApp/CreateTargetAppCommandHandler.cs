using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TargetApp.Commands.CreateTargetApp
{
    public class CreateTargetAppCommandHandler : IRequestHandler<CreateTargetAppCommand, Guid>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public CreateTargetAppCommandHandler(DataContext context, IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
            this._context = context;
        }

        public async Task<Guid> Handle(CreateTargetAppCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.UserId);
            var targetApp = new Domain.Entities.TargetApp
            {
                Interval = request.Interval,
                Name = request.Name,
                Url = request.Url,
                // CreatedById = user.Id,
                Created = DateTime.UtcNow,
                IsActive = request.IsActive
            };
            await _context.TargetApps.AddAsync(targetApp);
            await _context.SaveChangesAsync(cancellationToken);
            return targetApp.Id;
        }
    }
}