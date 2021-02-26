using System;
using Domain.Enums;
using MediatR;

namespace Application.HealthCheckResult.Commands.CreateHealthCheckResult
{
    public class CreateHealthCheckResultCommand : IRequest
    {
        public Guid TargetAppId { get; set; }
        public HealthCheckResultType Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}