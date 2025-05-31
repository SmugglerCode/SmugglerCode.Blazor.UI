using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Microsoft.AspNetCore.Components;
using System.Text;
using Threading = System.Threading.Tasks;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public partial class TypeClassCardView : ComponentBase
{
    [Parameter]
    public EventCallback<TypeClass> HandleCardSelection { get; set; }

    [Parameter]
    public TypeClassCardViewModel ViewModel { get; set; } = new TypeClassCardViewModel();

    public async Threading.Task HeaderClicked()
    {
        await HandleCardSelection.InvokeAsync(ViewModel.TypeClass);
    }
}

public class TypeClassCardViewModel
{
    public int DiagramClassId { get; set; } = 0;

    public string CardStyle { get; private set; } = string.Empty;

    public SizeInfo X { get; set; } = new(0, SizeUnit.Px);

    public SizeInfo Y { get; set; } = new(0, SizeUnit.Px);

    /// <summary>
    /// Gets or sets the TypeClass, which holds the information about a class.
    /// Properties, methods, ...
    /// </summary>
    public TypeClass TypeClass { get; set; } = null!;

    public void UpdateStyle()
    {
        var sb = new StringBuilder();

        sb.Append(CssStyleHelper.SetTop(Y));
        sb.Append(CssStyleHelper.SetLeft(X));
        sb.Append(CssStyleHelper.SetPosition("absolute"));

        CardStyle = sb.ToString();
    }
}
