using System;

namespace Web.Models
{
    public class JqueryCustomParameters
    {
        public int Start { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public SearchModel Search { get; set; }
        public Guid? TargetAppId { get; set; }
    }
}