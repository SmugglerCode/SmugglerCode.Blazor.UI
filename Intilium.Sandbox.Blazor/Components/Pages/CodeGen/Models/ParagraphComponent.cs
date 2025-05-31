using Intilium.Sandbox.Blazor.Components.Pages.Documentation.ComponentEditors;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models
{
    public sealed class ParagraphComponent : ComponentConfigurationBase
    {
        public override string GetComponentType()
        {
            return nameof(ParagraphComponent);
        }

        public override RenderFragment Render() => builder =>
        {
            builder.OpenComponent(0, typeof(Documentation.ParagraphComponent));
            builder.CloseComponent();
        };

        public override RenderFragment RenderEditor() => builder =>
        {
            builder.OpenComponent(0, typeof(ParagraphEditorComponent));
            builder.CloseComponent();
        };
    }
}
