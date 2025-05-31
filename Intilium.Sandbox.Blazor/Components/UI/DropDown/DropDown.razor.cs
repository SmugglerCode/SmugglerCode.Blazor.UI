using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.UI.DropDown
{
    public partial class DropDown<T> : ComponentBase
    {
        [Parameter]
        public bool ShowFilter { get; set; } = false;

        [Parameter]
        public List<T> Items { get; set; } = [];

        [Parameter]
        public T? SelectedItem { get; set; }

        [Parameter]
        public string PropertyName { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<T> SelectedItemChanged { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private bool showDropDownList = false;

        private string _filterText = string.Empty;

        private List<T> _filteredItems = [];

        private string _icon => showDropDownList ? "inti-triangle-down" : "inti-triangle-left";

        public string GetPropertyValue(T item)
        {
            var propInfo = typeof(T).GetProperty(PropertyName);
            var value = propInfo?.GetValue(item);
            return value as string ?? string.Empty;
        }

        public void Filter()
        {
            if (PropertyName != null && !string.IsNullOrWhiteSpace(_filterText))
            {
                _filteredItems = FilterHelper.FilterByProperty(Items, PropertyName, _filterText).ToList();
            }
            else
            {
                _filteredItems = Items;
            }
        }

        private void ToggleVisibility()
        {
            showDropDownList = !showDropDownList;
        }

        protected override void OnParametersSet()
        {
            _filteredItems = Items;
            Filter();
        }

        private void SelectItem(T item)
        {
            SelectedItem = item;
            SelectedItemChanged.InvokeAsync(item);
            showDropDownList = false;
        }
    }
}

public static class FilterHelper
{
    public static IEnumerable<T> FilterByProperty<T>(IEnumerable<T> source, string propertyName, string filterValue)
    {
        var prop = typeof(T).GetProperty(propertyName);
        if (prop == null) throw new ArgumentException($"Property '{propertyName}' bestaat niet in {typeof(T).Name}");

        var items = source.Where(x =>
        {
            var value = prop.GetValue(x);
            return value != null && value.ToString()!.ToLower().Contains(filterValue.ToLower());
        });
        return items;
    }
}