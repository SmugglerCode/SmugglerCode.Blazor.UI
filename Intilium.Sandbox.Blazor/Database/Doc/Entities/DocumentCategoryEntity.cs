namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    /// <summary>
    /// Represents the category for a document. I.e. the title that can be displayed in a list.
    /// The title can also be used to group other categories.
    /// </summary>
    public class DocumentCategoryEntity
    {
        /// <summary>
        /// Gets or sets the unique id for the document category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the category or document page.
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Gets or sets the parent id of a category <see cref="DocumentCategoryEntity"/>. This is another 
        /// category which will act as the parent category in order 
        /// to create a category tree.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the boolean which indicates if the category is the title for a document page. 
        /// If set to false then it is a category group.
        /// </summary>
        public bool IsDocumentationPage { get; set; }

        #region Navigation Properties

        /// <summary>
        /// gets or sets the navigational reference to the parent category, if any.
        /// </summary>
        public DocumentCategoryEntity? Parent { get; set; }

        /// <summary>
        /// Gets or sets the list of children categories if any.
        /// </summary>
        public List<DocumentCategoryEntity> Children { get; set; } = new List<DocumentCategoryEntity>();

        public bool HasChildren => Children != null && Children.Count > 0;

        #endregion
    }
}
