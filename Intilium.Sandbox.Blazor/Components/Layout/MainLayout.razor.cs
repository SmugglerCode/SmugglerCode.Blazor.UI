using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        private bool _prerendering = true;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _prerendering = false;
                StateHasChanged();
            }
        }
    }
}
