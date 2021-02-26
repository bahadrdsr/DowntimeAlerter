using System;
using MediatR;

namespace Application.TargetApp.Commands.UpdateTargetApp
{
    public class UpdateTargetAppCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public uint Interval { get; set; }
        public bool IsActive { get; set; }
    }
}