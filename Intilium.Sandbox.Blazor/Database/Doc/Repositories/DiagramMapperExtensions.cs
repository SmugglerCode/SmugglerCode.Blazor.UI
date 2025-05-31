using Intilium.Sandbox.Blazor.Database.Doc.Entities;

public static class DiagramMapperExtensions
{
    public static void UpdateExistingDiagram(DiagramEntity source, DiagramEntity destination)
    {
        destination.Name = source.Name;
        destination.CanvasWidth = source.CanvasWidth;
        destination.CanvasHeight = source.CanvasHeight;

        // insert new class diagrams
        destination.Classes.AddRange(source.Classes.Where(x => x.Id == 0));

        // update class diagrams
        foreach (var @class in source.Classes.Where(x => x.Id > 0))
        {
            var existingDiagramClass = destination.Classes.SingleOrDefault(x => x.Id == @class.Id);
            if (existingDiagramClass != null)
                UpdateExistingDiagramClass(@class, existingDiagramClass);
        }
    }

    public static void UpdateExistingDiagramClass(DiagramClassEntity source, DiagramClassEntity destination)
    {
        destination.TypeClassId = source.TypeClassId;
        destination.X = source.X;
        destination.Y = source.Y;
        destination.DiagramId = source.DiagramId;
    }
}
