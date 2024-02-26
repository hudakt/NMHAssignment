using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMHAssignment.Domain.Entities;

namespace NMHAssignment.Infrastructure.Persistance.Configurations
{
    internal class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }
}
