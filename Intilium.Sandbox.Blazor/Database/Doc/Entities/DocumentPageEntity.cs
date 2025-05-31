namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DocumentPageEntity
    {
        public int Id { get; set; }

        public int DocumentCategoryId { get; set; }

        public DocumentCategoryEntity DocumentCategory { get; set; } = null!;

        public List<DocumentPagePartEntity> PageParts { get; set; } = [];
    }
}
