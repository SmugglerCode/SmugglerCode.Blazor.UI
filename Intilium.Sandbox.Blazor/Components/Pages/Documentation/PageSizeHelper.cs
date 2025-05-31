namespace Intilium.Sandbox.Blazor.Components.Pages.Documentation;

public class PageSizeHelper
{
    public string MmToPixels(double mm, int dpi = 96)
    {
        // 25.4 = inch to mm
        var pixels = (mm / 25.4) * dpi;

        return $"{pixels}px";
    }
}
