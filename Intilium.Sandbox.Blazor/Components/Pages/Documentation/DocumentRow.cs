namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation;

public class DocumentRow
{
    #region properties
    public DocumentationPage Parent { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of column items, these items have a parent type of RowComponentµ
    /// </summary>
    public List<DocumentColumn> Columns { get; private set; } = [];

    /// <summary>
    /// Gets or sets the height of the row.
    /// Can be set as px, em, rem, ... e.g. 100px or 10rem.
    /// </summary>
    public string? Height { get; set; }

    /// <summary>
    /// Gets or sets the width of the row.
    /// Can be set as px, em, rem, ... e.g. 100px or 10rem.
    /// </summary>
    public string? Width { get; set; }

    /// <summary>
    /// The row number for the specific row.
    /// </summary>
    public int RowNumber { get; set; } = 0;

    #endregion

    #region constructor and life cycle methods

    public DocumentRow()
    {
        CreateCssStyle();
    }

    #endregion

    #region methods

    public void AddColumn(DocumentColumn column)
    {
        column.Parent = this;
        Columns.Add(column);
        CreateCssStyle();
    }

    #endregion

    public string CssStyle { get; private set; } = string.Empty;

    public void CreateCssStyle()
    {
        CssStyle = string.Empty;

        var height = string.IsNullOrWhiteSpace(Height) ? "height: 20em;" : $"height: {Height};";
        var width = string.IsNullOrWhiteSpace(Width) ? "width: 60em;" : $"width: {Width};";

        CssStyle += height;
        CssStyle += width;
    }
}