using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TargetApp.Queries.GetTargetAppById
{
    public class GetTargetAppByIdQueryHandler : IRequestHandler<GetTargetAppByIdQuery, TargetAppDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetTargetAppByIdQueryHandler(DataContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<TargetAppDto> Handle(GetTargetAppByIdQuery request, CancellationToken cancellationToken)
        {
            var targetApp = await _context.TargetApps.Where(x => x.Id == request.Id).ProjectTo<TargetAppDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return targetApp;
        }
    }
}