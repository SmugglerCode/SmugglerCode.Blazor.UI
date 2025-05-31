using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models
{
    public abstract class ComponentConfigurationBase
    {
        public abstract string GetComponentType();
        public abstract RenderFragment Render();
        public abstract RenderFragment RenderEditor();
    }
}
