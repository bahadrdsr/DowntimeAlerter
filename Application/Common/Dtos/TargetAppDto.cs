using System;
using Application.Common.Mappings;
using AutoMapper;

namespace Application.Common.Dtos
{
    public class TargetAppDto : IMapFrom<Domain.Entities.TargetApp>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }
        public uint Interval { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.TargetApp, TargetAppDto>();
        }
    }
}