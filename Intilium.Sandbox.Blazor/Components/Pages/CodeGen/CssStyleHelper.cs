namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public class CssStyleHelper
{
    public static string CssStyle<T>(string name, T value)
    {
        return $"{name}:{value};";
    }

    public static string SetTop(SizeInfo value)
    {
        return CssStyle("top", value);
    }

    public static string SetLeft(SizeInfo value)
    {
        return CssStyle("left", value);
    }

    public static string SetPosition(string value)
    {
        return CssStyle("position", value);
    }
}
