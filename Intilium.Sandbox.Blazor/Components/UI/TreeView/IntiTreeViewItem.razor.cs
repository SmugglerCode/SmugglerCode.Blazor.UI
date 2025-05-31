using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.TreeView
{
    public partial class IntiTreeViewItem<T> : ComponentBase
    {
        [Parameter]
        public List<TreeViewItem<T>> Items { get; set; } = [];

        [Parameter]
        public int Offset { get; set; } = 0;

        [Parameter]
        public int Step { get; set; } = 10;

        [Parameter]
        public string Measurement { get; set; } = "px";

        [Parameter]
        public EventCallback<T> SelectionChanged { get; set; }

        private string _rightPadding => Offset + Measurement;
        private void ToggleShowChildren(TreeViewItem<T> item)
        {
            item.ShowChildren = !item.ShowChildren;
        }

        private async Task SelectItem(TreeViewItem<T> item)
        {
            await SelectionChanged.InvokeAsync(item.Item);
        }

        private string GetTreeViewIcon(TreeViewItem<T> item)
        {
            return item.ShowChildren ? "inti-triangle-down" : "inti-triangle-right";
        }
    }

    public class TreeViewlist<T>
    {
        public List<TreeViewItem<T>> Items { get; set; } = [];
    }

    public class TreeViewItem<T>
    {
        public T Item { get; set; } = default!;

        public string Label { get; set; } = null!;

        public List<TreeViewItem<T>> Children { get; set; } = [];

        public bool HasChildren => Children != null && Children.Count > 0;

        public bool ShowChildren { get; set; } = false;
    }
}
