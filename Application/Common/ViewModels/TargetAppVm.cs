using System.Collections.Generic;
using Application.Common.Dtos;

namespace Application.Common.ViewModels
{
    public class TargetAppVm
    {
        public IList<TargetAppDto> Data { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}