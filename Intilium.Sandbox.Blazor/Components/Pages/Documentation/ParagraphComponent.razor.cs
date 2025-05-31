using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation;

public partial class ParagraphComponent : ComponentBase
{
    [Parameter]
    public ParagraphConfiguration? Configuration { get; set; }

    /// <summary>
    /// Gets or sets the width of the paragraph
    /// </summary>
    public string Width { get; set; } = "100%";
    public string Height { get; set; } = string.Empty;

    public ParagraphComponent()
    {
        CreateCssStyle();
    }

    public void CreateCssStyle()
    {
    }
}
