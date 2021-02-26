using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TargetApp.Queries.GetTargetApps
{
    public class GetTargetAppsQueryHandler : IRequestHandler<GetTargetAppsQuery, TargetAppVm>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetTargetAppsQueryHandler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<TargetAppVm> Handle(GetTargetAppsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TargetApps.AsQueryable();
            if (!string.IsNullOrEmpty(request.SearchText))
                query = query.Where(x => x.Name.ToLower().Contains(request.SearchText.ToLower()) || x.Url.ToLower().Contains(request.SearchText.ToLower()));
            var targetApps = await query.Skip((request.PageNo - 1) * request.PageSize).Take(request.PageSize)
            .ProjectTo<TargetAppDto>(_mapper.ConfigurationProvider).ToListAsync();
            var vm = new TargetAppVm
            {
                Data = targetApps,
                Count = targetApps.Count,
                PageNo = request.PageNo,
                PageSize = request.PageSize,
                TotalCount = query.Count()
            };
            return vm;
        }
    }
}