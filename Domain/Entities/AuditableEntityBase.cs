using System;

namespace Domain.Entities
{
    public abstract class AuditableEntityBase : EntityBase
    {
        public AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public DateTime Created { get; set; }
        public AppUser LastModifiedBy { get; set; }
        public string LastModifiedById { get; set; }

        public DateTime? LastModified { get; set; }


    }
}