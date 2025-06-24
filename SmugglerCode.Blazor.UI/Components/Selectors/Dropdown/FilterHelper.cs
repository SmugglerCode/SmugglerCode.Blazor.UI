namespace SmugglerCode.Blazor.UI.Components.Selectors;

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