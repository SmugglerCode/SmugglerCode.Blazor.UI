using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SmugglerCode.Blazor.UI.Components.Common;
using System.Text;

namespace SmugglerCode.Blazor.UI.Components.Inputs;

/// <summary>
/// Generic reusable input textbox component for Blazor.
/// Supports value binding, enter key handling, visibility control, and inherited disable state.
/// </summary>
/// <typeparam name="TValue">The data type of the input value (e.g., string, int, decimal).</typeparam>
public partial class TextBox<TValue> : DisabledScopeBase
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
    /// Defines whether the component is a text field, password field, .... This input type corresponds to the type of input in HTML.
    /// Default: text.
    /// </summary>
    [Parameter]
    public string InputType { get; set; } = "text";

    /// <summary>
    /// Determines whether the component is visible in the DOM.
    /// </summary>
    [Parameter]
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// The bound value of the input field.
    /// </summary>
    [Parameter]
    public TValue? Text { get; set; }

    /// <summary>
    /// Callback triggered when the value of the input changes.
    /// </summary>
    [Parameter]
    public EventCallback<TValue?> TextChanged { get; set; }

    /// <summary>
    /// Callback triggered when the Enter key is pressed.
    /// </summary>
    [Parameter]
    public EventCallback<TValue?> OnEnter { get; set; }

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
            await OnEnter.InvokeAsync(Text);
        }
    }

    /// <summary>
    /// Handles text input changes, converts the value to TValue and triggers the TextChanged callback.
    /// </summary>
    /// <param name="e">Change event args containing the new value.</param>
    private async Task OnTextChanged(ChangeEventArgs e)
    {
        if (e.Value == null)
        {
            Text = default;
            await TextChanged.InvokeAsync(Text);
            return;
        }

        var stringValue = e.Value.ToString();

        try
        {
            var convertedValue = (TValue?)Convert.ChangeType(stringValue, typeof(TValue));
            Text = convertedValue;
            await TextChanged.InvokeAsync(convertedValue);
        }
        catch
        {
            // Optional: log or handle conversion errors (e.g., invalid input).
            // TODO: to be implemented.
        }
    }

    private async Task SetFocusToInput()
    {
        await _inputRef.FocusAsync();
    }

    #endregion
}
