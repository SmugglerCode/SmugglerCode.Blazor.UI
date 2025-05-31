namespace Intilium.Sandbox.Blazor.Database.Doc
{
    /// <summary>
    /// Contains the text (paragraph) information for a documentation page.
    /// </summary>
    public class DocumentTextPart
    {
        /// <summary>
        /// Gets or sets the unique id for the document text part.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reference to the document page part <see cref="DocumentPagePart"/>. The documentation page part
        /// contains the layout information like row, column, width, height, span ...
        /// </summary>
        public int DocumentPagePartId { get; set; }

        /// <summary>
        /// Gets or sets the reference to the documentation page <see cref="DocumentPage"/>.
        /// </summary>
        public int DocumentPageId { get; set; }
    }
}