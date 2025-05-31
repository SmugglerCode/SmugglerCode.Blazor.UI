using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.Doc.EntityTypeConfigurations;

public class DocumentPageEntityTypeConfiguration : IEntityTypeConfiguration<DocumentPageEntity>
{
    public void Configure(EntityTypeBuilder<DocumentPageEntity> builder)
    {
        builder.ToTable("DocumentPage", "Doc");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.DocumentCategory)
            .WithMany()
            .HasForeignKey(x => x.DocumentCategoryId);
    }
}
