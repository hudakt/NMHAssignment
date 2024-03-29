﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMHAssignment.Domain.Entities;

namespace NMHAssignment.Infrastructure.Persistance.Configurations
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.Author)
                   .WithOne(a => a.Image)
                   .HasForeignKey<Author>(a => a.ImageId)
                   .IsRequired();
        }
    }
}
