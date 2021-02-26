using System;
using System.ComponentModel.DataAnnotations;
using Application.Common.Dtos;
using Application.Common.Mappings;
using AutoMapper;

namespace Web.Models
{
    public class TargetAppBindingModel : IMapFrom<TargetAppDto>
    {
        public Guid? Id { get; set; }
        [Required]
        [MaxLength(128)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MaxLength(512)]
        [RegularExpression(@"(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?")]
        public string Url { get; set; }
        public uint Interval { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TargetAppDto, TargetAppBindingModel>();
        }
    }
}