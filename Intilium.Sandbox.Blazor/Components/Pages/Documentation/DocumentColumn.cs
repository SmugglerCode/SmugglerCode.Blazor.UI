using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation;

public sealed class DocumentColumn
{
    #region fields
    #endregion

    #region properties

    /// <summary>
    /// Gets or sets the parent row, this should be the row to which this column exists.
    /// </summary>
    public DocumentRow Parent { get; set; } = null!;

    /// <summary>
    /// Gets or sets the row span: position / span.
    /// Used to set the column-span css setting.
    /// </summary>
    public int RowSpan { get; set; } = 1;

    /// <summary>
    /// Gets or sets the column span: position / span.
    /// Used to set the grid-row css setting.
    /// </summary>
    public int ColumnSpan { get; set; } = 1;

    public ComponentConfigurationBase? ChildComponent { get; set; }

    /// <summary>
    /// Gets or sets the position: position / span.
    /// Used to set the grid-row and grid column css setting.
    /// </summary>
    public int ColumnNumber { get; set; } = 1;

    public string CssStyle
    {
        get => GetCssStyle();
    }

    public bool IsActive { get; set; } = false;

    public void MakeActive()
    {
        Parent.Parent.SetActiveColumn(this);
    }

    private string GetCssStyle()
    {
        string style = string.Empty;

        style += $"grid-row:{Parent.RowNumber} / span {RowSpan};";
        style += $"grid-column:{ColumnNumber} / span {ColumnSpan};";

        return style;
    }

    #endregion
}