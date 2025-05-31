using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.Doc.EntityTypeConfigurations;

public class DiagramClassEntityTypeConfiguration : IEntityTypeConfiguration<DiagramClassEntity>
{
    public void Configure(EntityTypeBuilder<DiagramClassEntity> builder)
    {
        builder.ToTable("DiagramClass", "CodeGen");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Diagram)
            .WithMany()
            .HasForeignKey(x => x.DiagramId);
    }
}

