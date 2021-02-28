using System;
using System.Linq;
using System.Threading;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Account.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, CurrentUserDto>
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RegisterCommandHandler(DataContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async System.Threading.Tasks.Task<CurrentUserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                throw new BadRequestException("Email already exists");
            if (await _context.Users.Where(x => x.UserName == request.Email).AnyAsync())
                throw new BadRequestException("UserName already exists");

            var user = new AppUser
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                  return new CurrentUserDto
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                };
            }

            throw new Exception("Problem creating user");
        }
    }
}