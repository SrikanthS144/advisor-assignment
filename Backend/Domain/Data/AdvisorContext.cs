using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Domain.Data
{
    public partial class AdvisorContext : DbContext
    {
        private IHttpContextAccessor _httpContextAccessor;

        public virtual DbSet<Advisor> Advisor { get; set; } = null!;

        public AdvisorContext()
        {
        }

        public AdvisorContext(DbContextOptions<AdvisorContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advisor>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Deleted).HasDefaultValue(false);

                entity.Property(e => e.HealthStatus).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(8);

                entity.Property(e => e.Sin)
                    .HasMaxLength(9)
                    .HasColumnName("SIN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
