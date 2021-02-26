using System;
using MediatR;

namespace Application.TargetApp.Commands.CreateTargetApp
{
    public class CreateTargetAppCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public uint Interval { get; set; }
        public bool IsActive { get; set; }
        
        
    }
}