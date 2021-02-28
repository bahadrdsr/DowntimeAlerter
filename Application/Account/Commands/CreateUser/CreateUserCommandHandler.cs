using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Account.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;

        public CreateUserCommandHandler(UserManager<AppUser> userManager, DataContext context)
        {
            this._context = context;
            _userManager = userManager;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                UserName = request.UserName
            };

            var ir = await _userManager.CreateAsync(user, request.Password);//TO DO : CHECK FOR EXCEPTIONS
            if (ir.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return user.Id;
        }
    }
}