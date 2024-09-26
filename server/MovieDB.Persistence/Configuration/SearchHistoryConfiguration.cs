using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieDB.Core.Entities;

namespace MovieDB.Infrastructure.Configuration;

public class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{
    public void Configure(EntityTypeBuilder<SearchHistory> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.SearchTerm)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}