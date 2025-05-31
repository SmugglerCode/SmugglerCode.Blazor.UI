using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intilium.Sandbox.Blazor.Database.CodeGen.EntityTypeConfigurations;

public class TypeClassMethodEntityTypeConfiguration : IEntityTypeConfiguration<TypeClassMethod>
{
    public void Configure(EntityTypeBuilder<TypeClassMethod> builder)
    {
    }
}
