using Application.Common.Dtos;
using MediatR;

namespace Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<CurrentUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}