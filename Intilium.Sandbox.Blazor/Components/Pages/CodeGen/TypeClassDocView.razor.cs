using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen
{
    public partial class TypeClassDocView
    {
        [Parameter]
        public TypeClass? TypeClass { get; set; }

        [Parameter]
        public bool ShowPropertiesHeader { get; set; } = true;

        [Parameter]
        public EventCallback<TypeClassProperty> PropertySelectionChanged { get; set; }

        private async Task PropertySelected(TypeClassProperty property)
        {
            await PropertySelectionChanged.InvokeAsync(property);
        }
    }
}
