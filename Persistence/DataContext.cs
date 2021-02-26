using System;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Override default AspNet Identity table names
            modelBuilder.Entity<AppUser>(entity => { entity.ToTable(name: "Users"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: "UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: "UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: "UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: "RoleClaims"); });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
        public DbSet<TargetApp> TargetApps { get; set; }
        public DbSet<HealthCheckResult> HealthCheckResults { get; set; }

    }
}
