using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Created)
            .HasColumnType("SMALLDATETIME");
            
        builder.Property(p => p.LastModified)
            .HasColumnType("SMALLDATETIME");
    }
}
