using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class HealthCheckResultConfiguration : IEntityTypeConfiguration<HealthCheckResult>
    {
        public void Configure(EntityTypeBuilder<HealthCheckResult> builder)
        {
            builder.HasOne(x=>x.TargetApp).WithMany().HasForeignKey(z=>z.TargetAppId).IsRequired();
        }
    }
}