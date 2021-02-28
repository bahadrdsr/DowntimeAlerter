using Application.Common.Dtos;
using MediatR;

namespace Application.Account.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<CurrentUserDto>
    {

    }
}