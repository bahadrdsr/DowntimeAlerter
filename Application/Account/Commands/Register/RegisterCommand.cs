using System;
using Application.Common.Dtos;
using MediatR;

namespace Application.Account.Commands.Register
{
    public class RegisterCommand : IRequest<CurrentUserDto>
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public Guid? PhotoId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}