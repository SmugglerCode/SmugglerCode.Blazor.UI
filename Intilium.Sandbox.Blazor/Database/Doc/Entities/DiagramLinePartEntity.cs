namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DiagramLinePartEntity
    {
        public int Id { get; set; }
        public int DiagramLineId { get; set; }
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public DiagramLineEntity DiagramLine { get; set; } = null!;
    }
}
