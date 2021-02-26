using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid());
        }
    }
}