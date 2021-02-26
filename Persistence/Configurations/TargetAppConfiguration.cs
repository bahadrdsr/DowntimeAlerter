using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TargetAppConfiguration : IEntityTypeConfiguration<TargetApp>
    {
        public void Configure(EntityTypeBuilder<TargetApp> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Interval).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(512).IsRequired();
        }
    }
}