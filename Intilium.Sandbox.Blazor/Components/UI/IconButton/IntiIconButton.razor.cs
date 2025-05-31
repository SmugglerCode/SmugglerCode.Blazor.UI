using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.IconButton;

public partial class IntiIconButton : ComponentBase
{
    [Parameter]
    public EventCallback OnClicked { get; set; }

    [Parameter]
    public string? Label { get; set; }

    private async Task IconClicked()
    {
        await OnClicked.InvokeAsync();
    }
}
