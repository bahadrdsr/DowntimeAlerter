using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos
{
    public class AppUserDto : IMapFrom<AppUser>
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, AppUserDto>()
              .ForMember(d => d.Roles, opt => opt.Ignore());
        }
    }
}