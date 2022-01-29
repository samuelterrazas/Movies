using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(f => f.MovieId)
            .IsRequired();

        builder.Property(f => f.Url)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(f => f.Created)
            .HasColumnType("SMALLDATETIME");
            
        builder.Property(f => f.LastModified)
            .HasColumnType("SMALLDATETIME");
    }
}