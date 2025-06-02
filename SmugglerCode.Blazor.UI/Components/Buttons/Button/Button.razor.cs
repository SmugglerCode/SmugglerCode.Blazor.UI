using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SmugglerCode.Blazor.UI.Components.Common;

namespace SmugglerCode.Blazor.UI.Components.Buttons;

/// <summary>
/// A reusable button component that supports styling via <see cref="ButtonType"/>
/// and handles click events.
/// </summary>
public partial class Button : DisabledScopeBase
{
    #region private fields

    /// <summary>
    /// Computes the CSS class based on the specified <see cref="ButtonType"/>.
    /// </summary>
    private string CssClass => Type switch
    {
        ButtonType.Alert => "sc-button-alert",
        ButtonType.Primary => "sc-button-primary",
        _ => "sc-button-primary"
    };

    #endregion

    #region IsDisabled

    [Parameter]
    public bool? IsDisabled { get; set; }

    [CascadingParameter(Name = "IsDisabled")]
    public bool? CascadedIsDisabled { get; set; }

    public bool IsEffectivelyDisabled => ComputeEffectiveDisabled(IsDisabled, CascadedIsDisabled);

    private string CssDisabled => IsEffectivelyDisabled ? "sc-disabled" : string.Empty;

    #endregion

    #region parameters

    /// <summary>
    /// Gets or sets a value indicating whether the button is visible.
    /// When set to <c>false</c>, the button will not be rendered in the UI.
    /// </summary>
    [Parameter]
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// The text label displayed on the button.
    /// If empty, the <see cref="ChildContent"/> will be rendered instead.
    /// </summary>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// The button type that determines which CSS class is applied.
    /// </summary>
    [Parameter]
    public ButtonType Type { get; set; } = ButtonType.Primary;

    /// <summary>
    /// Event callback triggered when the button is clicked
    /// or activated via keyboard.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Alternative content to render inside the button if no <see cref="Label"/> is provided.
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    #endregion

    #region event handlers

    /// <summary>
    /// Invokes the <see cref="OnClick"/> callback.
    /// </summary>
    private async Task HandleClick()
    {
        if (IsEffectivelyDisabled)
            return;

        await OnClick.InvokeAsync();
    }

    /// <summary>
    /// Supports Enter and Space keys for accessibility keyboard actions.
    /// </summary>
    /// <param name="e">Keyboard event arguments.</param>
    private async Task HandleKeyUp(KeyboardEventArgs e)
    {
        if (IsEffectivelyDisabled)
            return;

        if (e.Key is "Enter" or " ")
        {
            await HandleClick();
        }
    }

    #endregion
}
