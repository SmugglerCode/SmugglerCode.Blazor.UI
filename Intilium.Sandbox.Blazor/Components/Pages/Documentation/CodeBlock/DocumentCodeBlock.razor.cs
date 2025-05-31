using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation.CodeBlock
{
    public partial class DocumentCodeBlock : ComponentBase
    {
        [Parameter]
        public string? Content { get; set; }

        private MarkupString StylizedContent = new();

        [Parameter]
        public List<DocumentCodeBlockItem> Keywords { get; set; } = [];

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Keywords.Add(new DocumentCodeBlockItem()
            {
                Name = "public",
                Styles = [new CssStyleItem() { Key = "color", Value = "red" }]
            });

            Keywords.Add(new DocumentCodeBlockItem()
            {
                Name = "string",
                Styles = [new CssStyleItem() { Key = "color", Value = "green" }]
            });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            var text = string.Empty;
            if (Content != null)
            {
                var tokens = Content.Split(" ");

                foreach (var token in tokens)
                {
                    var keyword = Keywords.SingleOrDefault(x => x.Name == token);
                    text += keyword == null ? $"{token} " : $"<span style='{keyword.Style}'>{token}</span> ";
                }
            }

            StylizedContent = (MarkupString)text;
        }
    }
}
