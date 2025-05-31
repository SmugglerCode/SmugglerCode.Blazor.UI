namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public class SizeInfo
{
    public decimal Size { get; set; } = 0;

    public SizeUnit Unit { get; set; } = SizeUnit.Px;

    public override string ToString()
    {
        if (Unit == SizeUnit.Percent)
        {
            return $"{Size}%";
        }

        return $"{Size}{Unit.ToString().ToLower()}";
    }

    public SizeInfo(decimal size, SizeUnit unit)
    {
        Size = size;
        Unit = unit;
    }
}
