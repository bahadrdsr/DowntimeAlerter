using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Enums;

namespace Application.Common.Dtos
{
    public class HealthCheckResultDto : IMapFrom<Domain.Entities.HealthCheckResult>
    {
        public Guid Id { get; set; }
        public TargetAppDto TargetApp { get; set; }
        public DateTime ExecutionTime { get; set; }
        public HealthCheckResultType Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.HealthCheckResult, HealthCheckResultDto>()
            .ForMember(x => x.TargetApp, opt => opt.MapFrom(s => s.TargetApp));
        }
    }
}