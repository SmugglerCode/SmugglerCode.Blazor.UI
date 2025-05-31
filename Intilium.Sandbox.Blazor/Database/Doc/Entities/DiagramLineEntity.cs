namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DiagramLineEntity
    {
        public int Id { get; set; }

        public int DiagramId { get; set; }

        public string Color { get; set; } = "black";

        public int LineWidth { get; set; } = 1;

        public List<DiagramLinePartEntity> DiagramLineParts { get; set; } = [];

        public DiagramEntity Diagram { get; set; } = null!;
    }
}
