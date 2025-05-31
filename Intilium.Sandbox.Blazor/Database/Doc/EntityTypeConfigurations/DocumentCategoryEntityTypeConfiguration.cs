using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.Doc.EntityTypeConfigurations;

public partial class DocumentCategoryEntityTypeConfiguration : IEntityTypeConfiguration<DocumentCategoryEntity>
{
    public void Configure(EntityTypeBuilder<DocumentCategoryEntity> builder)
    {
        builder.ToTable("DocumentCategory", "Doc");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired()
                                      .HasMaxLength(200);

        builder.Property(x => x.IsDocumentationPage)
           .IsRequired();

        builder
        .HasOne(x => x.Parent)
        .WithMany(x => x.Children)
        .HasForeignKey(x => x.ParentId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
