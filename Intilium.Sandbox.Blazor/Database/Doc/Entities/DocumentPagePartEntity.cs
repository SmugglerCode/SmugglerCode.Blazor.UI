namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DocumentPagePartEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reference to the page where the part belongs to.
        /// </summary>
        public int DocumentPageId { get; set; }

        public short Row { get; set; }

        public short Column { get; set; }

        public short RowSpan { get; set; }

        public short ColumnSpan { get; set; }

        public string Width { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        #region navigation properties

        public DocumentPageEntity DocumentPage { get; set; } = null!;

        #endregion

        #region todo: to be remove

        public bool IsActive { get; set; } = false;

        public string CssStyle
        {
            get => GetCssStyle();
        }

        private string GetCssStyle()
        {
            string style = string.Empty;

            style += $"grid-row:{Row} / span {RowSpan};";
            style += $"grid-column:{Column} / span {ColumnSpan};";
            style += $"width:{Width};";

            return style;
        }

        #endregion
    }
}
