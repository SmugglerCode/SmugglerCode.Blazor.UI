using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Sandbox.Components.Pages
{
    public partial class Home : ComponentBase
    {
        private bool _show = true;
        private string _customerName = "John Doe";

        private void Toggle()
        {
            _show = !_show;
            StateHasChanged();
        }
    }
}
