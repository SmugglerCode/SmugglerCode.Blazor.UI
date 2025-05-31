namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation;

public class DocumentPage
{
    /// <summary>
    /// Gets or sets the rows per document page.
    /// </summary>
    public List<DocumentRow> Rows { get; set; } = [];

    //public static DocumentPage ToViewModel(DocumentPageEntity entity)
    //{
    //    var doc = new DocumentPage();

    //    var groups = entity.PageParts.GroupBy(x => x.Row);

    //    foreach (var group in groups) 
    //    {
    //        var number = group.First();
    //        doc.Rows.Add(new DocumentRow()
    //        {
    //        });
    //    }

    //    return doc;
    //}
}
