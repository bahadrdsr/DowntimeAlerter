using System.Linq;
using System.Threading;
using Application.Common.Dtos;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.HealthCheckResult.Queries.GetHealthCheckResults
{
    public class GetHealthCheckResultsQueryHandler : IRequestHandler<GetHealthCheckResultsQuery, HeathCheckResultVm>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetHealthCheckResultsQueryHandler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async System.Threading.Tasks.Task<HeathCheckResultVm> Handle(GetHealthCheckResultsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.HealthCheckResults.AsQueryable();
            if (request.TargetAppId.HasValue)
                query = query.Where(x => x.TargetAppId == request.TargetAppId.Value);
            var results = await query.Skip((request.PageNo - 1) * request.PageSize).Take(request.PageSize)
             .ProjectTo<HealthCheckResultDto>(_mapper.ConfigurationProvider).ToListAsync();
            var vm = new HeathCheckResultVm
            {
                Data = results,
                Count = results.Count,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                TotalCount = query.Count()
            };
            return vm;
        }
    }
}