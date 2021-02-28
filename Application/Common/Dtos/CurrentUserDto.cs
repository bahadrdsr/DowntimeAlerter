using System.Collections.Generic;

namespace Application.Common.Dtos
{
    public class CurrentUserDto
    {
         public CurrentUserDto()
        {
            Roles = new List<string>();
            Claims = new List<string>();
        }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public IList<string> Claims { get; private set; }
        public IList<string> Roles { get; private set; }
    }
}