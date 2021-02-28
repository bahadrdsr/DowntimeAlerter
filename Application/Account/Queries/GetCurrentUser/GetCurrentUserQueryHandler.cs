using System.Threading;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;
        public GetCurrentUserQueryHandler(UserManager<AppUser> userManager, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _userManager = userManager;
        }
        public async System.Threading.Tasks.Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userAccessor.UserId);
            var currentUser = new CurrentUserDto
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                currentUser.Roles.Add(role);

            return currentUser;
        }
    }
}