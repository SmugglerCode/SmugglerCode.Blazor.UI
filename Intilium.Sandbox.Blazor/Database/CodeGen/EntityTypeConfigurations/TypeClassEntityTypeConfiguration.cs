using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.EntityTypeConfigurations
{

    public class TypeClassEntityTypeConfiguration : IEntityTypeConfiguration<TypeClass>
    {
        public void Configure(EntityTypeBuilder<TypeClass> builder)
        {
            builder.ToTable("TypeClass", "CodeGen");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.IsPrimitive)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.Namespace)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
