using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.Doc.EntityTypeConfigurations;

public class DocumentPagePartEntityTypeConfiguration : IEntityTypeConfiguration<DocumentPagePartEntity>
{
    public void Configure(EntityTypeBuilder<DocumentPagePartEntity> builder)
    {
        builder.ToTable("DocumentPagePart", "Doc");
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.IsActive);

        builder.HasOne(x => x.DocumentPage)
            .WithMany(x => x.PageParts)
            .HasForeignKey(x => x.DocumentPageId);
    }
}
