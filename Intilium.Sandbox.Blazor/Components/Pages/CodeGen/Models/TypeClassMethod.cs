namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen.Models;

/// <summary>
/// Represents a method of a specific type (class).
/// </summary>
public sealed class TypeClassMethod
{
    /// <summary>
    /// Gets or sets the unique identifier for the method of a specific type (class).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the type (class) to which the method belongs.
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Gets or sets the access modifier.
    /// </summary>
    public string AccessModifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the method return type.
    /// </summary>
    public string ReturnType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the method name.
    /// </summary>
    public string MethodName { get; set; } = null!;

    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the method parameters.
    /// </summary>
    public List<TypeClassMethodParameter> Parameters { get; set; } = [];
}
