using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.DataGrid
{
    public enum DataGridCellType
    {
        Text,
        CheckBox
    }

    public partial class IntiDataGrid<TItem> : ComponentBase
    {
        [Parameter]
        public IEnumerable<TItem> Items { get; set; } = [];

        [Parameter]
        public bool AutoGenerateColumns { get; set; } = false;

        // Ontvangt de child-columns
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public string GridTemplateColumns { get; set; } = string.Empty;

        // Interne lijst van kolommen
        private List<IntiDataGridColumn<TItem>> Columns { get; set; } = new();

        /// <summary>
        /// Children will register them self, this will allow us to add columns using the IntiDataGridColumn.
        /// </summary>
        /// <param name="column">The column to be added to the list of columns.</param>
        public void RegisterColumn(IntiDataGridColumn<TItem> column)
        {
            if (column != null && !Columns.Contains(column))
            {
                Columns.Add(column);
                GridTemplateColumns += $" {column.Width}";
                StateHasChanged();
            }
        }

        protected override void OnInitialized()
        {
            // Genereer automatisch kolommen via Reflection indien ingesteld
            if (AutoGenerateColumns && typeof(TItem) is not null)
            {
                //foreach (var prop in properties)
                //{
                //    if (!Columns.Any(c => c.Property == prop.Name))
                //    {
                //        Columns.Add(new IntiDataGridColumn<TItem>()
                //        {
                //            Label = prop.Name,
                //            Property = prop.Name
                //        });
                //    }
                //}
            }
        }

        // Haalt de waarde van een property via Reflection
        private static object? GetPropertyValue(object item, string propertyName)
        {
            var prop = item.GetType().GetProperty(propertyName);
            return prop?.GetValue(item, null);
        }
    }
}

