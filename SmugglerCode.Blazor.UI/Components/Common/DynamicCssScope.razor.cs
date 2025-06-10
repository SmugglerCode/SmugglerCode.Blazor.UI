using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Components.Common;

public partial class DynamicCssScope
{
    [Parameter]
    public bool IsDynamicSize { get; set; } = true;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
