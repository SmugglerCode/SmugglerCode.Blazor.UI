using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.Popup
{
    public partial class IntiPopup : ComponentBase
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the popup window.
        /// </summary>
        [Parameter]
        public bool IsVisible { get; set; }
        private void CloseWindow(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            IsVisible = false;
        }
    }
}
