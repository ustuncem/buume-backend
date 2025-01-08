using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BUUME.Domain.Files;
using File = BUUME.Domain.Files.File;
using Path = BUUME.Domain.Files.Path;

namespace BUUME.Infrastructure.Configurations;

internal sealed class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("files");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Name(value))
            .IsRequired();
        
        builder.Property(x => x.Path).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Path(value))
            .IsRequired();
        
        builder.Property(x => x.Type).HasMaxLength(500)
            .HasConversion(name => name.Value, value => FileType.Create(value))
            .IsRequired();
        
        builder.Property(x => x.Size).HasMaxLength(500)
            .HasConversion(name => name.Value, value => new Size(value))
            .IsRequired();
        
        builder.HasQueryFilter(t => t.DeletedAt == null);
    }
}