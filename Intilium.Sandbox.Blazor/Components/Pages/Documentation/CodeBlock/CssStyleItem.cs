namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation.CodeBlock
{
    public class CssStyleItem
    {
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;

        public string Style => $"{Key}: {Value}";

        public override string ToString()
        {
            return Style;
        }
    }
}
