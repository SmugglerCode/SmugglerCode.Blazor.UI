using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Sandbox.Components.Pages;
public partial class Home : ComponentBase
{
    private bool _show = true;
    private string _customerName = "John Doe";

    private bool _isDynamicSize = false;

    private string CssStyles => CreateCssStyle();

    private int _size = 16;

    private string CreateCssStyle()
    {
        return $"font-size: {_size}px";
    }

    private void Toggle()
    {
        _show = !_show;
        StateHasChanged();
    }

    private void ToggleDynamicSize()
    {
        _isDynamicSize = !_isDynamicSize;
    }

    private void Increment() { _size += 4; }
    private void Decrement() { _size = _size == 4 ? 4 : _size - 4; }
}
