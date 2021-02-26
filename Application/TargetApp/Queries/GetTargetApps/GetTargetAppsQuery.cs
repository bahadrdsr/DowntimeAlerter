using Application.Common.ViewModels;
using MediatR;

namespace Application.TargetApp.Queries.GetTargetApps
{
    public class GetTargetAppsQuery : IRequest<TargetAppVm>
    {
        public GetTargetAppsQuery(int pageNo, int pageSize, string searchText = "")
        {
            PageNo = pageNo;
            PageSize = pageSize;
            SearchText = searchText;
        }
        public string SearchText { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}