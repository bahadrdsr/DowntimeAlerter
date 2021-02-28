using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Account.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UserListVm>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public GetAllUsersQueryHandler(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<UserListVm> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            // .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
            var usrList = new List<AppUserDto>();
            foreach (AppUser usr in users)
            {
               var roles  = await _userManager.GetRolesAsync(usr);
               var dto  = _mapper.Map<AppUserDto>(usr);
               dto.Roles = roles;
               usrList.Add(dto);
            }
            var vm = new UserListVm
            {
                Users = usrList,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                Count = users.Count
            };
            return vm;
        }
    }
}