using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class HealthCheckResult : EntityBase
    {
        public TargetApp TargetApp { get; set; }
        public Guid TargetAppId { get; set; }
        public DateTime ExecutionTime { get; set; }
        public HealthCheckResultType Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}