using Intilium.Sandbox.Blazor.Components.Pages.CodeGen;
using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Intilium.Sandbox.Blazor.Database.Doc.Entities;

public sealed class DiagramCanvasViewModel
{
    public DiagramEntity CurrentDiagram { get; set; } = null!;

    public TypeClass? SelectedTypeClass { get; set; }
    public List<TypeClass> TypeClasses { get; set; } = [];
    public string CanvasCss { get; private set; } = string.Empty;
    public List<TypeClassCardViewModel> Cards { get; set; } = [];
    public string DiagramName { get; set; } = string.Empty;
    public SizeInfo CanvasWidth { get; set; } = new(100, SizeUnit.Percent);
    public SizeInfo CanvasHeight { get; set; } = new(2000, SizeUnit.Px);

    public Point? PreviousPoint { get; set; }
    public List<Point> LinePoints { get; private set; } = [];

    public TypeClassCardViewModel? SelectedCard { get; set; }
    public Point AddPoint(double x, double y, bool straightCorner = false)
    {
        if (straightCorner && PreviousPoint != null)
        {
            var deltaX = Math.Abs(PreviousPoint.X - x);
            var deltaY = Math.Abs(PreviousPoint.Y - y);
            x = deltaX < deltaY ? PreviousPoint.X : x;
            y = deltaY < deltaX ? PreviousPoint.Y : y;
        }

        PreviousPoint = new Point(x, y);
        LinePoints.Add(PreviousPoint);
        return PreviousPoint;
    }

    public void UpdateCanvasSize(SizeInfo width, SizeInfo height)
    {
        CanvasCss = $"width:{width};height:{height}";
    }
}
