using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Movies.Domain.Entities.File;

namespace Movies.Infrastructure.Persistence.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
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