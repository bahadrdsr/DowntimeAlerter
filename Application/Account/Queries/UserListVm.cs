using System.Collections.Generic;
using Application.Common.Dtos;

namespace Application.Account.Queries
{
    public class UserListVm
    {
        public List<AppUserDto> Users { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
    }
}