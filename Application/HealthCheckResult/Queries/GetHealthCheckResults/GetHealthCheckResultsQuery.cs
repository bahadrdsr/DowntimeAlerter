using System;
using Application.Common.ViewModels;
using MediatR;

namespace Application.HealthCheckResult.Queries.GetHealthCheckResults
{
    public class GetHealthCheckResultsQuery : IRequest<HeathCheckResultVm>
    {
        public GetHealthCheckResultsQuery(int pageNo, int pageSize, Guid? targetAppId = null)
        {
            this.PageNo = pageNo;
            this.PageSize = pageSize;
            this.TargetAppId = targetAppId;
        }
        public Guid? TargetAppId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

    }
}