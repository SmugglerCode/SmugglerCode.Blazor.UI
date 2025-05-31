using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.CheckBox;

public partial class IntiCheckBox
{
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public bool IsChecked { get; set; }

    [Parameter]
    public EventCallback<bool> IsCheckedChanged { get; set; }

    public async Task HandleClick(bool isChecked)
    {
        IsChecked = isChecked;
        await IsCheckedChanged.InvokeAsync(IsChecked);
    }
}
