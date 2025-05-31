using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.AspNetCore.Components;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public partial class TypeClassList
{
    /// <summary>
    /// Gets or sets the search text, used for filtering the list of TypeClasses.
    /// </summary>
    public string SearchText { get; set; } = string.Empty;

    /// <summary>
    /// The list of type classes used for filtering.
    /// </summary>
    [Parameter]
    public List<TypeClass> TypeClasses { get; set; } = [];

    private List<TypeClass> _filteredTypeClasses = [];

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        FilterList();
    }

    public void FilterList() 
    {
        if (string.IsNullOrWhiteSpace(SearchText)) 
        {
            _filteredTypeClasses = TypeClasses; 
        }

        _filteredTypeClasses = TypeClasses.Where(x => x.Name.Contains(SearchText.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
    }
}
