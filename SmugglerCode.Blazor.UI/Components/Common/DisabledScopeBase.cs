using Microsoft.AspNetCore.Components;

namespace SmugglerCode.Blazor.UI.Components.Common
{
    public abstract class DisabledScopeBase : ComponentBase
    {
        protected bool ComputeEffectiveDisabled(bool? explicitValue, bool? cascadedValue)
        => explicitValue ?? cascadedValue ?? false;
    }
}
