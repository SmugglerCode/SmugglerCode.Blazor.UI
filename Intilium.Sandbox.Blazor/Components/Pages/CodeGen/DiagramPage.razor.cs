using Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;
using Intilium.Sandbox.Blazor.Components.UI.Snackbar;
using Intilium.Sandbox.Blazor.Database.CodeGen.Repositories;
using Intilium.Sandbox.Blazor.Database.Doc.Entities;
using Intilium.Sandbox.Blazor.Database.Doc.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json.Serialization;

namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public partial class DiagramPage : ComponentBase
{
    #region Injections

    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    protected ITypeClassRepository _typeClassRepository { get; set; } = null!;

    [Inject]
    protected IDiagramRepository _diagramRepository { get; set; } = null!;

    [Inject]
    protected ITypeClassPropertyRepository _propertyRepository { get; set; } = null!;

    [Inject]
    private IntiSnackbarService SnackbarService { get; set; } = null!;

    #endregion

    private TypeClassProperty _selectedProperty { get; set; } = new TypeClassProperty();
    private bool _autoSave = true;
    private bool _showMessage = false;
    private string _message = string.Empty;

    public TypeClass CurrentTypeClass { get; set; } = new TypeClass();

    public List<DiagramEntity> Diagrams { get; set; } = [];

    public List<string> Namespaces { get; set; } = [];
    public List<TypeClassCardViewModel> TypeClasses { get; set; } = [];

    public DiagramCanvasViewModel CanvasViewModel { get; set; } = new();

    private TypeClassFilter _filter = new TypeClassFilter();

    private void SetSelectedProperty(TypeClassProperty property)
    {
        _selectedProperty = property;
        PropertyExpression = property.TypeName + " " + property.Name;
    }

    private async Task UpdateDatabase()
    {
        var typeClasses = await _typeClassRepository.GetTypeClassesAsync();
    }

    private async Task FilterByNamespace(string @namespace)
    {
        _filter.Namespace = new FilterInfo<string>()
        {
            FilterType = FilterType.Contains,
            Value = @namespace
        };
        await LoadTypeClasses();
    }

    private async Task ShowAlert(string text)
    {
        await JSRuntime.InvokeVoidAsync("alert", text);
    }

    /// <summary>
    /// Gets or sets the property expression, a property expression has the format "{type} {propertyName}".
    /// </summary>
    public string PropertyExpression { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the method expression, a method expression has the format "{scope} {type} {method name}({parameters})" where parameters is of the form {type} {parameterName}, ...
    /// </summary>
    public string MethodExpression { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTypeClasses();
        await GetAllNamespacesAsync();
        await UpdateDatabase();
        await LoadDiagrams();

        CanvasViewModel.TypeClasses = TypeClasses.Select(x => x.TypeClass).ToList();
    }

    public async Task LoadDiagrams()
    {
        Diagrams = await _diagramRepository.GetAllDiagramsAsync();
    }

    public async Task GetAllNamespacesAsync()
    {
        Namespaces = await _typeClassRepository.GetAllNamespacesAsync();
    }

    public void SelectCardType(TypeClass typeClass)
    {
        CurrentTypeClass = typeClass;
    }

    public async Task LoadTypeClasses()
    {
        var types = await _typeClassRepository.GetTypeClassesAsync(_filter);
        TypeClasses = types.Select(t => new TypeClassCardViewModel() { TypeClass = t }).ToList();
    }

    public async Task AddProperty()
    {
        if (string.IsNullOrWhiteSpace(PropertyExpression))
        {
            _message = "Gelieve eerst de property gegevens in te vullen.";
            _showMessage = true;
        }
        else
        {
            var tokens = PropertyExpression.Split(" ");
            var propType = tokens[0];

            _selectedProperty.Name = tokens[1];
            _selectedProperty.TypeName = tokens[0];

            if (_selectedProperty.Id == 0)
            {
                CurrentTypeClass.Properties.Add(_selectedProperty);
            }

            if (_autoSave)
            {
                await _typeClassRepository.UpdateAsync(CurrentTypeClass);
            }
        }

        _selectedProperty = new TypeClassProperty();
        PropertyExpression = string.Empty;
    }

    public void Cancel()
    {
        SnackbarService.Show("Bezig met het opslagen van de klasse ...");

        CurrentTypeClass = new TypeClass();

        // Clear the property fields
        _selectedProperty = new TypeClassProperty();
        PropertyExpression = string.Empty;

    }

    public void AddTypeClass()
    {


        var isExisting = TypeClasses.Any(x => x.TypeClass.Name == CurrentTypeClass.Name);

        if (!isExisting)
        {
            TypeClasses.Add(new TypeClassCardViewModel()
            {
                TypeClass = CurrentTypeClass
            });
        }

        if (CurrentTypeClass.Id == 0)
        {
            _typeClassRepository.InsertAsync(CurrentTypeClass);
        }
        else
        {
            _typeClassRepository.UpdateAsync(CurrentTypeClass);
        }
    }

    public void AddMethod()
    {
        if (string.IsNullOrWhiteSpace(MethodExpression)) return;

        string methodSignature = MethodExpression;

        // Stap 1: Splits op '(' om de methode-informatie en parameters te scheiden
        string[] parts = methodSignature.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        string methodInfo = parts[0].Trim();
        string parametersInfo = parts.Length > 1 ? parts[1].Trim() : "";

        // Stap 2: Splits de methode-informatie op spaties om access modifier, return type en methodenaam te verkrijgen
        string[] methodParts = methodInfo.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string accessModifier = methodParts.Length > 2 ? methodParts[0] : "";
        string returnType = methodParts.Length > 2 ? methodParts[1] : methodParts[0];
        string methodName = methodParts.Last();

        // Stap 3: Verwerk de parameters
        List<TypeClassMethodParameter> parameters = new List<TypeClassMethodParameter>();
        if (!string.IsNullOrWhiteSpace(parametersInfo))
        {
            string[] parameterParts = parametersInfo.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var param in parameterParts)
            {
                var paramDetails = param.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (paramDetails.Length == 2)
                {
                    parameters.Add(new TypeClassMethodParameter { Type = paramDetails[0], Name = paramDetails[1] });
                }
            }
        }

        var md = new TypeClassMethod
        {
            AccessModifier = accessModifier,
            ReturnType = returnType,
            MethodName = methodName,
            Parameters = parameters
        };

        CurrentTypeClass.Methods.Add(md);
    }
    private async Task CreateDiagram()
    {
        if (_diagramRepository != null)
        {
            var diagram = new DiagramEntity()
            {
                Name = CanvasViewModel.DiagramName,
                CanvasHeight = CanvasViewModel.CanvasHeight.Size,
                CanvasHeightUnit = CanvasViewModel.CanvasHeight.Unit,
                CanvasWidth = CanvasViewModel.CanvasWidth.Size,
                CanvasWidthUnit = CanvasViewModel.CanvasWidth.Unit
            };
            diagram.Id = await _diagramRepository.InsertAsync(diagram);
            CanvasViewModel.CurrentDiagram = diagram;

            // TODO remove since I set the current diagram
            CanvasViewModel.CanvasWidth = new(diagram.CanvasWidth, diagram.CanvasWidthUnit);
            CanvasViewModel.CanvasHeight = new(diagram.CanvasHeight, diagram.CanvasHeightUnit);
        }
    }
    private void DiagramSelectionChanged(DiagramEntity diagram)
    {
        CanvasViewModel.CurrentDiagram = diagram;
        CanvasViewModel.CanvasHeight = new(diagram.CanvasHeight, SizeUnit.Px);
        CanvasViewModel.CanvasWidth = new(diagram.CanvasWidth, SizeUnit.Px);

        CanvasViewModel.Cards.Clear();

        foreach (var @class in diagram.Classes)
        {
            var card = new TypeClassCardViewModel()
            {
                DiagramClassId = @class.Id,
                TypeClass = @class.TypeClass,
                X = new SizeInfo(@class.X, SizeUnit.Px),
                Y = new SizeInfo(@class.Y, SizeUnit.Px),
            };
            card.UpdateStyle();
            CanvasViewModel.Cards.Add(card);
        }

        // TODO remove since I set the CurrentDiagram
        CanvasViewModel.CanvasWidth = new(diagram.CanvasWidth, diagram.CanvasWidthUnit);
        CanvasViewModel.CanvasHeight = new(diagram.CanvasHeight, diagram.CanvasHeightUnit);
    }
    private void ShowSnackbar()
    {
        SnackbarService.Show("tesdt");
    }
}

public enum MessageType
{
    Info = 0,
    Warning = 1,
    Error = 2
}

public class MessageInfo
{
    /// <summary>
    /// Gets the message.
    /// </summary>
    public string Message { get; private set; } = null!;

    /// <summary>
    /// Gets the message type, like info, warning, error.
    /// </summary>
    public MessageType Type { get; private set; } = MessageType.Info;

    public MessageInfo(string message, MessageType type = MessageType.Info)
    {
        Message = message;
        Type = type;
    }
}

public abstract class TransferData
{
    public List<MessageInfo> Messages { get; private set; }

    public List<MessageInfo> Errors => Messages.Where(x => x.Type == MessageType.Error).ToList();

    public bool HasErrors => Messages.Any(x => x.Type == MessageType.Error);

    protected TransferData()
    {
        Messages = [];
    }

    protected void AddMessage(string message, MessageType type = MessageType.Info)
    {
        var msg = new MessageInfo(message, type);
        Messages.Add(msg);
    }

    protected void AddError(string message)
    {
        AddMessage(message, MessageType.Error);
    }
}

public class PropertyAnalyze : TransferData
{
    /// <summary>
    /// Gets the property name of the property.
    /// </summary>
    public string? PropertyName { get; private set; }

    /// <summary>
    /// Gets the type of the property.
    /// </summary>
    public TypeClass? TypeClass { get; private set; }

    private string _propertyExpression = null!;
    private string _unanalizedType = null!;

    public PropertyAnalyze(string propertyExpression)
    {
        _propertyExpression = propertyExpression;
        SplitPropertyData();
        AnalyzePropertyType();
    }

    /// <summary>
    /// A complex type is one with multiple types like List<MyType>.
    /// </summary>
    /// <returns></returns>
    public bool IsComplexType => _propertyExpression.Contains("<");

    /// <summary>
    /// Split the expression in a type and the property name.
    /// </summary>
    private void SplitPropertyData()
    {
        if (string.IsNullOrWhiteSpace(_propertyExpression))
        {
            AddError("Geen geldige property gegevens: er is niets ingevuld.");
            return;
        }

        var tokens = _propertyExpression.Split(" ");
        if (tokens.Length != 2)
        {
            AddError("Geen geldige property gegevens: na het splitsen zouden er 2 delen moeten zijn, het property type en de naam.");
            return;
        }

        PropertyName = tokens[1];
        _unanalizedType = tokens[0];
    }

    private void AnalyzePropertyType()
    {
        if (IsComplexType)
        {
            var types = SplitGenericTypes(_unanalizedType);
            //TypeClass = types.Last();
        }
        else
        {

        }
    }

    private List<string> SplitGenericTypes(string input)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        int depth = 0;

        foreach (char c in input)
        {
            if (c == '<')
            {
                if (depth == 0)
                {
                    result.Add(current.ToString().Trim());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
                depth++;
            }
            else if (c == '>')
            {
                depth--;
                if (depth == 0)
                {
                    // Recurse voor nested types
                    result.AddRange(SplitGenericTypes(current.ToString()));
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            else if (c == ',' && depth == 1)
            {
                // scheiding tussen top-level generieke parameters
                result.AddRange(SplitGenericTypes(current.ToString()));
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        if (depth == 0 && current.Length > 0)
        {
            result.Add(current.ToString().Trim());
        }

        return result;
    }
}

public class MessageResult
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("messageType")]
    public MessageType MessageType { get; set; }

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    public MessageResult(string message, MessageType messageType, bool isSuccess)
    {
        Message = message;
        MessageType = messageType;
        IsSuccess = isSuccess;
    }
}
