using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.EntityTypeConfigurations;

public class TypeClassPropertyEntityTypeConfiguration : IEntityTypeConfiguration<TypeClassProperty>
{
    public void Configure(EntityTypeBuilder<TypeClassProperty> builder)
    {
        builder.ToTable("TypeClassProperty", "CodeGen");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.HasGetter);
        builder.Property(x => x.HasSetter);

        builder.Property(x => x.TypeClassId).IsRequired(true);

        builder.HasOne(tp => tp.TypeClass)
        .WithMany(tc => tc.Properties)
        .HasForeignKey(tp => tp.TypeClassId)
        .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(tp => tp.PropertyType)
        //    .WithMany()
        //    .HasForeignKey(tp => tp.PropertyTypeId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}

public class TypeClassEntityTypeConfiguration : IEntityTypeConfiguration<TypeClass>
{
    public void Configure(EntityTypeBuilder<TypeClass> builder)
    {
        builder.ToTable("TypeClass", "CodeGen");

        builder.HasKey(x => x.Id);

    }
}