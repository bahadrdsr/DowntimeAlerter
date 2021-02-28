using System;
using MediatR;

namespace Application.Account.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid? PhotoId { get; set; }
    }
}