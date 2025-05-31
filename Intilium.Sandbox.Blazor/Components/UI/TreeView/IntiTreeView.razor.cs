using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.TreeView
{
    public partial class IntiTreeView<T> : ComponentBase
    {
        [Parameter]
        public List<TreeViewItem<T>> Items { get; set; } = [];

        [Parameter]
        public EventCallback<T> SelectionChanged { get; set; }

        [Parameter]
        public int Offset { get; set; } = 0;

        [Parameter]
        public int step { get; set; } = 10;

        [Parameter]
        public string Measurement { get; set; } = "rem";
        private async Task SelectionHasChanged(T item)
        {
            await SelectionChanged.InvokeAsync(item);
        }
    }
}
