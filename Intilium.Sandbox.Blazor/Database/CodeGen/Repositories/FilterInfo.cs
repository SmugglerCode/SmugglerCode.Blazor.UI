namespace Intilium.Sandbox.Blazor.Database.CodeGen.Repositories
{
    public class FilterInfo<T>
    {
        public FilterType FilterType { get; set; } = FilterType.Contains;
        public T? Value { get; set; }
        public bool HasValue => Value != null;
    }
}
