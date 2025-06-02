using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Components.Common;

public partial class DisabledScope
{
    [Parameter]
    public bool IsDisabled { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
