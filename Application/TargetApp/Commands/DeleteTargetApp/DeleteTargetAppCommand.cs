using System;
using MediatR;

namespace Application.TargetApp.Commands.DeleteTargetApp
{
    public class DeleteTargetAppCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}