using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMHAssignment.Domain.Entities;

namespace NMHAssignment.Infrastructure.Persistance.Configurations
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasIndex(a => a.Title);

            builder.HasMany(a => a.Author)
                   .WithMany(a => a.Article)
                   .UsingEntity(j => j.ToTable("AuthorArticles"));

            builder.HasOne(a => a.Site)
                   .WithMany(s => s.Article)
                   .HasForeignKey(a => a.SiteId)
                   .IsRequired();
        }
    }
}
