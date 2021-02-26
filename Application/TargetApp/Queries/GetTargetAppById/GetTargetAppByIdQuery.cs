using System;
using Application.Common.Dtos;
using MediatR;

namespace Application.TargetApp.Queries.GetTargetAppById
{
    public class GetTargetAppByIdQuery : IRequest<TargetAppDto>
    {
        public Guid Id { get; set; }

    }
}