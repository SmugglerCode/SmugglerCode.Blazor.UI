using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.DataGrid
{
    public partial class IntiDataGridColumn<TItem> : ComponentBase
    {
        [CascadingParameter]
        public IntiDataGrid<TItem>? Parent { get; set; }

        [Parameter]
        public string Title { get; set; } = string.Empty;

        [Parameter]
        public string Property { get; set; } = string.Empty;

        [Parameter]
        public DataGridCellType CellType { get; set; } = DataGridCellType.Text;


        [Parameter]
        public string Width { get; set; } = "auto";

        protected override void OnInitialized()
        {
            Parent?.RegisterColumn(this);
        }
    }
}
