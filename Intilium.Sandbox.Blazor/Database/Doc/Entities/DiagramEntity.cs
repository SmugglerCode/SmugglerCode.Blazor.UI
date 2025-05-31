using Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

namespace Intilium.Sandbox.Blazor.Database.Doc.Entities
{
    public class DiagramEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal CanvasWidth { get; set; } = 0;
        public decimal CanvasHeight { get; set; } = 0;
        public SizeUnit CanvasHeightUnit { get; set; }
        public SizeUnit CanvasWidthUnit { get; set; }
        public List<DiagramClassEntity> Classes { get; set; } = [];
    }
}
