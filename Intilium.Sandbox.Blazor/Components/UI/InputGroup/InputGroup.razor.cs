using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.InputGroup
{
    public partial class InputGroup : ComponentBase
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
