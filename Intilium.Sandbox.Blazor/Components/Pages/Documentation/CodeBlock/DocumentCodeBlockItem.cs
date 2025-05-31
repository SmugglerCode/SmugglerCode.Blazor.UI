namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation.CodeBlock
{
    public class DocumentCodeBlockItem
    {
        public string Name { get; set; } = null!;
        public List<CssStyleItem> Styles { get; set; } = [];
        public string Style => Styles == null ? "" : CreateStyle();

        private string CreateStyle()
        {
            var css = string.Empty;
            foreach (var style in Styles)
            {
                css += $"{style.Style};";
            }
            return css;
        }
    }
}
