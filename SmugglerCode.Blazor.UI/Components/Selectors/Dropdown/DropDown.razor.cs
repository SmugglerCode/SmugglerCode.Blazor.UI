using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SmugglerCode.Blazor.UI.Components.Common;
using SmugglerCode.Blazor.UI.Components.Inputs;
using System.Text;

namespace SmugglerCode.Blazor.UI.Components.Selectors;

public partial class DropDown<T> : DisabledScopeBase, IAsyncDisposable
{
    #region private variables

    /// <summary>
    /// Reference to the searchFilter text box.
    /// </summary>
    private TextBox _searchFilter = null!;

    private bool _showDropDownList = false;
    private DotNetObjectReference<DropDown<T>>? _selfRef;
    private IJSObjectReference? _dropdownScriptReference;
    private ElementReference _wrapper;
    private ElementReference _input;
    private List<ElementReference> _itemsRef = new List<ElementReference>();
    private List<T> _filteredItems = [];

    private string IconClass => _showDropDownList ? "inti-triangle-down" : "inti-triangle-left";

    // because the ref is null when the dropdown part is not shown, wen showing the dropdown, the textbox reference will be filled in after the renderpass, so use this to check the onafterrender part.
    private bool _focusPending = false;

    #endregion

    #region injections

    [Inject] IJSRuntime JS { get; set; } = default!;

    #endregion

    #region parameters

    [Parameter]
    public RenderFragment<T>? ItemTemplate { get; set; }

    [Parameter]
    public bool ShowFilter { get; set; } = false;

    [Parameter]
    public List<T> Items { get; set; } = [];

    [Parameter]
    public T? SelectedItem { get; set; }

    [Parameter]
    public string PropertyName { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<T> SelectedItemChanged { get; set; }

    [Parameter]
    public string NoItems { get; set; } = "Geen items";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

    #region private methods

    private string GetPropertyValue(T item)
    {
        var propInfo = typeof(T).GetProperty(PropertyName);
        var value = propInfo?.GetValue(item);
        return value as string ?? string.Empty;
    }
    private void Filter(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            _filteredItems = Items;
        }
        else if (!string.IsNullOrWhiteSpace(PropertyName))
        {
            _filteredItems = FilterHelper.FilterByProperty(Items, PropertyName, text).ToList();
        }
        else
        {
            _filteredItems = Items.Where(x => x!.ToString()!.Contains(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        EnsureRefsLength();
    }

    private string CreateCssClasses()
    {
        var sb = new StringBuilder();

        if (IsEffectivelyDisabled)
            sb.Append("sc-disabled ");

        sb.Append(IsDynamicSizing ? "dynamic-size " : "fixed-size ");

        return sb.ToString();
    }


    #endregion

    #region javascript to C# calls

    [JSInvokable]
    public Task Close()
    {
        _showDropDownList = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    #endregion

    #region lifecycle methods

    protected override void OnParametersSet()
    {
        _filteredItems = Items;
        EnsureRefsLength();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_focusPending)
            await _searchFilter.FocusAsync();

        _focusPending = false;
    }

    #endregion

    private void EnsureRefsLength()
    {
        if (_itemsRef.Count != _filteredItems.Count)
            _itemsRef = Enumerable.Repeat(default(ElementReference), _filteredItems.Count)
                                   .ToList();
    }

    #region EventHandlers

    private async Task ToggleVisibilityHandler(bool? value = null)
    {
        if (IsEffectivelyDisabled)
            return;

        _filteredItems = Items;
        EnsureRefsLength();

        if (value == null)
            _showDropDownList = !_showDropDownList;
        else
            _showDropDownList = value.Value;

        if (_showDropDownList)
        {
            _dropdownScriptReference ??= await JS.InvokeAsync<IJSObjectReference>("import", "./_content/SmugglerCode.Blazor.UI/js/smugglercodedropdownhelper.js");

            _selfRef ??= DotNetObjectReference.Create(this);
            await _dropdownScriptReference.InvokeVoidAsync("register", _wrapper, _selfRef);
        }

        if (_showDropDownList == false && _dropdownScriptReference is not null)
            await _dropdownScriptReference.InvokeVoidAsync("unregister", _wrapper);
    }

    private void SelectItemHandler(T item)
    {
        if (IsEffectivelyDisabled)
            return;

        SelectedItem = item;
        SelectedItemChanged.InvokeAsync(item);
        _showDropDownList = false;
    }

    private async Task KeyEventHandler(KeyboardEventArgs e)
    {
        if (IsEffectivelyDisabled)
            return;

        bool ctrl = e.CtrlKey || e.MetaKey;
        if (ctrl && e.Key == "z")
        {
            _focusPending = true;
            await ToggleVisibilityHandler(true);
        }

        if (_showDropDownList)
        {
            _dropdownScriptReference ??= await JS.InvokeAsync<IJSObjectReference>("import", "./_content/SmugglerCode.Blazor.UI/js/smugglercodedropdownhelper.js");

            _selfRef ??= DotNetObjectReference.Create(this);
            await _dropdownScriptReference.InvokeVoidAsync("register", _wrapper, _selfRef);
        }

        //if (e.Key == "ArrowDown" && index < _filteredItems.Count - 1)
        //    await _refs[index + 1].FocusAsync();

        //if (e.Key == "ArrowUp" && index > 0)
        //    await _refs[index - 1].FocusAsync();
    }

    private async Task DropDownItemKeyHandler(KeyboardEventArgs e, T item, int index)
    {
        if (IsEffectivelyDisabled)
            return;

        if (e.Key == "Enter")
        {
            SelectItemHandler(item);
        }

        if (e.Key == "ArrowDown")
        {
            await JS.InvokeVoidAsync("focusHelper.next", _itemsRef[index]);
        }
    }

    #endregion

    public async ValueTask DisposeAsync()
    {
        await ToggleVisibilityHandler(false);
        _selfRef?.Dispose();

        if (_dropdownScriptReference is not null)
            await _dropdownScriptReference.DisposeAsync();
    }
}
