using Blazored.TextEditor;
using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation.ComponentEditors
{
    public partial class ParagraphEditorComponent : ComponentBase
    {
        [Parameter]
        public ParagraphConfiguration? Configuration { get; set; }

        /// <summary>
        /// Gets or sets the rich tectbox editor settings.
        /// </summary>
        private BlazoredTextEditor? textEditor;

        public async Task UpdateConfigurationContent()
        {
            if (Configuration != null && textEditor != null)
            {
                Configuration.Content = (MarkupString)(await textEditor.GetHTML());
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && textEditor != null && Configuration != null)
            {
                await textEditor.LoadHTMLContent(Configuration.Content.ToString());
            }
        }
    }
}
