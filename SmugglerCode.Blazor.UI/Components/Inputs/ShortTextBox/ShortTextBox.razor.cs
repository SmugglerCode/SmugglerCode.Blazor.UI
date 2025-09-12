using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SmugglerCode.Blazor.UI.Components.Common;
using System.Text;

namespace SmugglerCode.Blazor.UI.Components.Inputs;
public partial class ShortTextBox : DisabledScopeBase
{
    #region private fields

    /// <summary>
    /// Reference to the HTML input element, used for focusing programmatically.
    /// </summary>
    private ElementReference _inputRef;

    #endregion

    #region IsDisabled

    /// <summary>
    /// Indicates whether this component is disabled explicitly.
    /// </summary>
    [Parameter]
    public bool? IsDisabled { get; set; }

    [CascadingParameter(Name = "IsDynamicSizing")]
    public bool IsDynamicSizing { get; set; }

    /// <summary>
    /// Indicates whether this component is disabled via a parent cascading value.
    /// </summary>
    [CascadingParameter(Name = "IsDisabled")]
    public bool? CascadedIsDisabled { get; set; }

    /// <summary>
    /// Effective disabled state: explicit > inherited > default (false).
    /// </summary>
    public bool IsEffectivelyDisabled => ComputeEffectiveDisabled(IsDisabled, CascadedIsDisabled);

    /// <summary>
    /// Returns the CSS class for disabled state styling.
    /// </summary>
    private string CssClasses => CreateCssClasses();

    #endregion

    #region parameters

    /// <summary>
    /// Allow the user to set an custom icon, this can be an icon from the smugglercode font library or a third party library.
    /// Note that the size for a third party library could require css changes which are not available.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Determines whether the component is visible in the DOM.
    /// </summary>
    [Parameter]
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// The bound value of the input field.
    /// </summary>
    [Parameter]
    public short Value { get; set; } = 0;

    /// <summary>
    /// Callback triggered when the value of the input changes.
    /// </summary>
    [Parameter]
    public EventCallback<short> ValueChanged { get; set; }

    /// <summary>
    /// Callback triggered when the Enter key is pressed.
    /// </summary>
    [Parameter]
    public EventCallback<short> OnEnter { get; set; }

    /// <summary>
    /// Callback triggeren when clicking on the self set icon <see cref="Icon"/>
    /// </summary>
    [Parameter]
    public EventCallback<short> OnIconPressed { get; set; }

    #endregion

    #region public methods

    /// <summary>
    /// Sets focus to the input element.
    /// </summary>
    public async Task FocusAsync()
    {
        await _inputRef.FocusAsync();
    }

    #endregion

    #region private methods

    private string CreateCssClasses()
    {
        var sb = new StringBuilder();

        if (IsEffectivelyDisabled)
            sb.Append("sc-disabled ");

        sb.Append(IsDynamicSizing ? "dynamic-size " : "fixed-size ");

        return sb.ToString();
    }

    /// <summary>
    /// Handles the Enter key press and triggers the OnEnter callback if not disabled.
    /// </summary>
    /// <param name="e">Keyboard event args.</param>
    private async Task OnKeyPressed(KeyboardEventArgs e)
    {
        if (!IsEffectivelyDisabled && e.Key == "Enter")
        {
            await OnEnter.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// Handles text input changes, converts the value to TValue and triggers the TextChanged callback.
    /// </summary>
    /// <param name="e">Change event args containing the new value.</param>
    private async Task OnTextChanged(ChangeEventArgs e)
    {
        Value = ParseValue(e.Value);
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task ClearPressedEventHandler()
    {
        if (IsEffectivelyDisabled) return;

        Value = 0;
        await ValueChanged.InvokeAsync(Value);
    }

    private short ParseValue(object? val)
    {
        if (val == null) return 0;

        return short.TryParse(val.ToString(), out var value) ? value : (short)0;
    }

    private async Task IconPressedEventHandler()
    {
        if (IsEffectivelyDisabled) return;

        await OnIconPressed.InvokeAsync(Value);
    }

    private async Task SetFocusToInput()
    {
        await _inputRef.FocusAsync();
    }

    #endregion
}
