using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.Doc.EntityTypeConfigurations;

public partial class DocumentCategoryEntityTypeConfiguration;

public class DiagramEntityTypeConfiguration : IEntityTypeConfiguration<DiagramEntity>
{
    public void Configure(EntityTypeBuilder<DiagramEntity> builder)
    {
        builder.ToTable("Diagram", "CodeGen");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

        builder.HasMany(x => x.Classes)
            .WithOne(x => x.Diagram)
            .HasForeignKey(x => x.DiagramId);
    }
}

