using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models
{
    public sealed class ParagraphConfiguration : ComponentConfigurationBase
    {
        public MarkupString Content { get; set; } = (MarkupString)"Dit is <span style='color:red;'>mijn</span> tekst";

        public override string GetComponentType()
        {
            return nameof(ParagraphComponent);
        }

        public override RenderFragment Render() => builder =>
        {
            builder.OpenComponent(0, typeof(Documentation.ParagraphComponent));
            builder.AddAttribute(1, "Configuration", this);
            builder.CloseComponent();
        };

        public override RenderFragment RenderEditor() => builder =>
        {
            //builder.OpenComponent(0, typeof(ParagraphEditorComponent));
            //builder.AddAttribute(1, "Configuration", this);
            //builder.CloseComponent();
        };
    }
}
